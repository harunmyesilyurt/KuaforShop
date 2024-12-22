using AutoMapper;
using KuaforShop.Application.DTOs.EmployeeDTOs;
using KuaforShop.Core.Entities;
using KuaforShop.Core.Enumertaions;
using KuaforShop.Persistence.Repositories.Employee;
using KuaforShop.Persistence.Repositories.Expertise;
using Microsoft.EntityFrameworkCore;

namespace KuaforShop.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IExpertiseRepository _expertiseRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IExpertiseRepository expertiseRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _expertiseRepository = expertiseRepository;
        }

        public async Task<bool> ChangePasswordAsync(Guid id, string oldPassword, string newPassword)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null || employee.Password != oldPassword)
                return false;

            employee.Password = newPassword;
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveAsync();
            return true;
        }

        public async Task<bool> CreateAsync(CreateEmployeeDTO createEmployeeDTO)
        {
            var existingEmployee = await _employeeRepository.GetByUsernameAsync(createEmployeeDTO.Username);
            if (existingEmployee != null)
                return false;

            var employee = _mapper.Map<Employees>(createEmployeeDTO);
            var result = await _employeeRepository.AddAsync(employee);
            if (!result)
                return false;

            await _employeeRepository.SaveAsync();

            // Uzmanlık alanlarını ekle
            if (createEmployeeDTO.Expertise?.Any() == true)
            {
                await _expertiseRepository.AddRangeForEmployeeAsync(employee.Id, createEmployeeDTO.Expertise);
            }

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _employeeRepository.RemoveAsync(id);
            await _employeeRepository.SaveAsync();
            return result;
        }

        public async Task<List<EmployeeDTO>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAll().ToListAsync();
            return _mapper.Map<List<EmployeeDTO>>(employees);
        }

        public async Task<List<EmployeeDTO>> GetByRoleAsync(enmRoles role)
        {
            var employees = await _employeeRepository.GetByRoleAsync(role);
            return _mapper.Map<List<EmployeeDTO>>(employees);
        }

        public async Task<List<EmployeeDTO>> GetBySaloonAsync(Guid saloonId)
        {
            var employees = await _employeeRepository.GetBySaloonAsync(saloonId);
            return _mapper.Map<List<EmployeeDTO>>(employees);
        }

        public async Task<EmployeeDTO> GetByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<bool> UpdateAsync(Guid id, CreateEmployeeDTO updateEmployeeDTO)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return false;

            if (employee.Username != updateEmployeeDTO.Username)
            {
                var existingEmployee = await _employeeRepository.GetByUsernameAsync(updateEmployeeDTO.Username);
                if (existingEmployee != null)
                    return false;
            }

            employee.Username = updateEmployeeDTO.Username;
            employee.Name = updateEmployeeDTO.Name;
            employee.Surname = updateEmployeeDTO.Surname;
            employee.Sex = updateEmployeeDTO.Sex;
            employee.SaloonId = updateEmployeeDTO.SaloonId;
            employee.Role = updateEmployeeDTO.Role;
            employee.BeginTime = updateEmployeeDTO.BeginTime;
            employee.EndTime = updateEmployeeDTO.EndTime;
            employee.WorkDays = updateEmployeeDTO.WorkDays;

            if (!string.IsNullOrEmpty(updateEmployeeDTO.Password))
            {
                employee.Password = updateEmployeeDTO.Password;
            }

            _employeeRepository.Update(employee);
            await _employeeRepository.SaveAsync();
            return true;
        }
    }
}