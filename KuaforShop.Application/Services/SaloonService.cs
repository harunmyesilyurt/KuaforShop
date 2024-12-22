using AutoMapper;
using KuaforShop.Application.DTOs.SaloonDTOs;
using KuaforShop.Core.Entities;
using KuaforShop.Persistence.Repositories.Saloon;
using Microsoft.EntityFrameworkCore;

namespace KuaforShop.Application.Services
{
    public class SaloonService : ISaloonService
    {
        private readonly ISaloonRepository _saloonRepository;
        private readonly IMapper _mapper;

        public SaloonService(ISaloonRepository saloonRepository, IMapper mapper)
        {
            _saloonRepository = saloonRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(CreateSaloonDTO createSaloonDTO)
        {
            var saloon = _mapper.Map<Saloons>(createSaloonDTO);
            await _saloonRepository.AddAsync(saloon);
            await _saloonRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _saloonRepository.RemoveAsync(id);
            await _saloonRepository.SaveAsync();
            return result;
        }

        public async Task<List<SaloonDTO>> GetAllAsync()
        {
            var saloons = await _saloonRepository.GetAll().ToListAsync();
            return _mapper.Map<List<SaloonDTO>>(saloons);
        }

        public async Task<SaloonDTO> GetByIdAsync(Guid id)
        {
            var saloon = await _saloonRepository.GetByIdAsync(id);
            return _mapper.Map<SaloonDTO>(saloon);
        }

        public async Task<List<SaloonDTO>> GetByWorkDaysAsync(int workDays)
        {
            var saloons = await _saloonRepository.GetByWorkDaysAsync(workDays);
            return _mapper.Map<List<SaloonDTO>>(saloons);
        }

        public async Task<bool> UpdateAsync(Guid id, CreateSaloonDTO updateSaloonDTO)
        {
            var saloon = await _saloonRepository.GetByIdAsync(id);
            if (saloon == null)
                return false;

            saloon.Name = updateSaloonDTO.Name;
            saloon.Address = updateSaloonDTO.Address;
            saloon.Phone = updateSaloonDTO.Phone;
            saloon.OpenTime = updateSaloonDTO.OpenTime;
            saloon.CloseTime = updateSaloonDTO.CloseTime;
            saloon.WorkDays = updateSaloonDTO.WorkDays;
            saloon.SaloonType = updateSaloonDTO.SaloonType;

            _saloonRepository.Update(saloon);
            await _saloonRepository.SaveAsync();
            return true;
        }
    }
}