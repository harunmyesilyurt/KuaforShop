using AutoMapper;
using KuaforShop.Application.DTOs.AppointmentDTOs;
using KuaforShop.Core.Entities;
using KuaforShop.Core.Enumertaions;
using KuaforShop.Persistence.Repositories.Appointment;
using KuaforShop.Persistence.Repositories.Employee;
using KuaforShop.Persistence.Repositories.Service;
using Microsoft.EntityFrameworkCore;

namespace KuaforShop.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            IEmployeeRepository employeeRepository,
            IServiceRepository serviceRepository,
            IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _employeeRepository = employeeRepository;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<bool> CancelAsync(Guid id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
                return false;

            appointment.Status = enmAppointmentStatus.Cancelled;
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveAsync();
            return true;
        }

        public async Task<bool> CreateAsync(CreateAppointmentDTO createAppointmentDTO)
        {
            try
            {
                // Çalışan ve hizmet kontrolü
                var employee = await _employeeRepository.GetByIdAsync(createAppointmentDTO.EmployeeId);
                var service = await _serviceRepository.GetByIdAsync(createAppointmentDTO.ServiceId);

                if (employee == null || service == null)
                    return false;

                // Çalışanın müsaitlik kontrolü
                var isAvailable = await IsEmployeeAvailableAsync(
                    createAppointmentDTO.EmployeeId,
                    createAppointmentDTO.Date,
                    service.Duration);

                if (!isAvailable)
                    return false;

                var appointment = new Appointments
                {
                    UserId = createAppointmentDTO.UserId,
                    ServiceId = createAppointmentDTO.ServiceId,
                    EmployeeId = createAppointmentDTO.EmployeeId,
                    Date = createAppointmentDTO.Date,
                    Status = enmAppointmentStatus.Waiting
                };

                var result = await _appointmentRepository.AddAsync(appointment);
                if (!result)
                    return false;

                await _appointmentRepository.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<AppointmentDTO>> GetAllAsync()
        {
            var query = _appointmentRepository.GetAll();

            try
            {
                query = query.Include("User")
                            .Include("Service")
                            .Include("Employee");

                var appointments = await query.ToListAsync();
                return _mapper.Map<List<AppointmentDTO>>(appointments);
            }
            catch (Exception ex)
            {
                // Hata durumunda basit listeyi döndür
                var appointments = await _appointmentRepository.GetAll().ToListAsync();
                return _mapper.Map<List<AppointmentDTO>>(appointments);
            }
        }

        public async Task<List<DateTime>> GetAvailableTimeSlotsAsync(Guid employeeId, DateTime date)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                return new List<DateTime>();

            var availableSlots = new List<DateTime>();
            var startTime = new DateTime(date.Year, date.Month, date.Day, employee.BeginTime.Hour, 0, 0);
            var endTime = new DateTime(date.Year, date.Month, date.Day, employee.EndTime.Hour, 0, 0);

            while (startTime < endTime)
            {
                if (await IsEmployeeAvailableAsync(employeeId, startTime, 30)) // 30 dakikalık slotlar
                {
                    availableSlots.Add(startTime);
                }
                startTime = startTime.AddMinutes(30);
            }

            return availableSlots;
        }

        public async Task<List<AppointmentDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var appointments = await _appointmentRepository.GetByDateRangeAsync(startDate, endDate);
            return _mapper.Map<List<AppointmentDTO>>(appointments);
        }

        public async Task<List<AppointmentDTO>> GetByEmployeeAsync(Guid employeeId)
        {
            var appointments = await _appointmentRepository.GetByEmployeeAsync(employeeId);
            return _mapper.Map<List<AppointmentDTO>>(appointments);
        }

        public async Task<AppointmentDTO> GetByIdAsync(Guid id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            return _mapper.Map<AppointmentDTO>(appointment);
        }

        public async Task<List<AppointmentDTO>> GetByStatusAsync(enmAppointmentStatus status)
        {
            var appointments = await _appointmentRepository.GetByStatusAsync(status);
            return _mapper.Map<List<AppointmentDTO>>(appointments);
        }

        public async Task<List<AppointmentDTO>> GetByUserAsync(Guid userId)
        {
            var appointments = await _appointmentRepository.GetByUserAsync(userId);
            return _mapper.Map<List<AppointmentDTO>>(appointments);
        }

        public async Task<bool> IsEmployeeAvailableAsync(Guid employeeId, DateTime date, int duration)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                return false;

            // TimeSpan'i TimeOnly'ye çeviriyoruz
            var appointmentTime = TimeOnly.FromTimeSpan(date.TimeOfDay);
            var endTime = appointmentTime.AddMinutes(duration);

            // Çalışma saatleri kontrolü
            if (appointmentTime < employee.BeginTime || endTime > employee.EndTime)
                return false;

            // Çakışma kontrolü
            return !await _appointmentRepository.HasOverlappingAppointmentAsync(
                employeeId,
                date,
                date.AddMinutes(duration));
        }

        public async Task<bool> UpdateStatusAsync(Guid id, enmAppointmentStatus status)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
                return false;

            appointment.Status = status;
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveAsync();
            return true;
        }

        public async Task<int> GetTotalAppointmentsAsync()
        {
            return await _appointmentRepository.GetAll().CountAsync();
        }

        public async Task<int> GetTodaysAppointmentsCountAsync()
        {
            var today = DateTime.Today;
            return await _appointmentRepository.GetAll()
                .CountAsync(x => x.Date.Date == today);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            var completedAppointments = await _appointmentRepository.GetAll()
                .Include(x => x.Service)
                .Where(x => x.Status == enmAppointmentStatus.Approved)
                .ToListAsync();

            return completedAppointments.Sum(x => (decimal)x.Service.Price);
        }

        public async Task<List<AppointmentDTO>> GetRecentAppointmentsAsync(int count)
        {
            var appointments = await _appointmentRepository.GetAll()
                .Include(x => x.User)
                .Include(x => x.Service)
                .Include(x => x.Employee)
                .OrderByDescending(x => x.Date)
                .Take(count)
                .ToListAsync();

            return _mapper.Map<List<AppointmentDTO>>(appointments);
        }
    }
}