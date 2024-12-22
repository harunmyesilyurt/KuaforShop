using KuaforShop.Application.DTOs.EmployeeDTOs;
using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Application.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> GetByIdAsync(Guid id);
        Task<List<EmployeeDTO>> GetAllAsync();
        Task<List<EmployeeDTO>> GetBySaloonAsync(Guid saloonId);
        Task<List<EmployeeDTO>> GetByRoleAsync(enmRoles role);
        Task<bool> CreateAsync(CreateEmployeeDTO createEmployeeDTO);
        Task<bool> UpdateAsync(Guid id, CreateEmployeeDTO updateEmployeeDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ChangePasswordAsync(Guid id, string oldPassword, string newPassword);
    }
}