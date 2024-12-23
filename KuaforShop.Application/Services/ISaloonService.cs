using KuaforShop.Application.DTOs.SaloonDTOs;

namespace KuaforShop.Application.Services
{
    public interface ISaloonService
    {
        Task<SaloonDTO> GetByIdAsync(Guid id);
        Task<List<SaloonDTO>> GetAllAsync();
        Task<List<SaloonDTO>> GetByWorkDaysAsync(int workDays);
        Task<bool> CreateAsync(CreateSaloonDTO createSaloonDTO);
        Task<bool> UpdateAsync(Guid id, CreateSaloonDTO updateSaloonDTO);
        Task<bool> DeleteAsync(Guid id);
    }
}