using AutoMapper;
using KuaforShop.Application.DTOs.ServiceDTOs;
using KuaforShop.Core.Entities;
using KuaforShop.Persistence.Repositories.Service;
using Microsoft.EntityFrameworkCore;

namespace KuaforShop.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(CreateServiceDTO createServiceDTO)
        {
            var service = _mapper.Map<KuaforShop.Core.Entities.Services>(createServiceDTO);
            await _serviceRepository.AddAsync(service);
            await _serviceRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _serviceRepository.RemoveAsync(id);
            await _serviceRepository.SaveAsync();
            return result;
        }

        public async Task<List<ServiceDTO>> GetAllAsync()
        {
            var services = await _serviceRepository.GetAll().ToListAsync();
            return _mapper.Map<List<ServiceDTO>>(services);
        }

        public async Task<ServiceDTO> GetByIdAsync(Guid id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            return _mapper.Map<ServiceDTO>(service);
        }

        public async Task<List<ServiceDTO>> GetBySaloonAsync(Guid saloonId)
        {
            var services = await _serviceRepository.GetBySaloonAsync(saloonId);
            return _mapper.Map<List<ServiceDTO>>(services);
        }

        public async Task<bool> UpdateAsync(Guid id, CreateServiceDTO updateServiceDTO)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null)
                return false;

            service.Name = updateServiceDTO.Name;
            service.Price = updateServiceDTO.Price;
            service.Duration = updateServiceDTO.Duration;
            service.DurationType = updateServiceDTO.DurationType;
            service.SaloonId = updateServiceDTO.SaloonId;

            _serviceRepository.Update(service);
            await _serviceRepository.SaveAsync();
            return true;
        }
    }
}