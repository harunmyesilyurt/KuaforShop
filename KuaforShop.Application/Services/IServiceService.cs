using KuaforShop.Application.DTOs.ServiceDTOs;

namespace KuaforShop.Application.Services
{
    public interface IServiceService
    {
        Task<ServiceDTO> GetByIdAsync(Guid id);
        Task<List<ServiceDTO>> GetAllAsync();
        Task<List<ServiceDTO>> GetBySaloonAsync(Guid saloonId);
        Task<bool> CreateAsync(CreateServiceDTO createServiceDTO);
        Task<bool> UpdateAsync(Guid id, CreateServiceDTO updateServiceDTO);
        Task<bool> DeleteAsync(Guid id);
    }
}