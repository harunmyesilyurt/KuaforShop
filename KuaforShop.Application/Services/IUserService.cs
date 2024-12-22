using KuaforShop.Application.DTOs.UserDTOs;
using KuaforShop.Core.Enumertaions;
//using KuaforShop.Web.Models;

namespace KuaforShop.Application.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(CreateUserDTO createUserDTO);
        Task<bool> UpdateAsync(Guid id, CreateUserDTO updateUserDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<UserDTO> ValidateUserAsync(LoginDTO loginDTO);
        Task<int> GetTotalUsersAsync();
        Task<List<UserDTO>> GetByRoleAsync(enmRoles role);
        //Task<bool> UpdateProfileAsync(Guid id, UserProfileViewModel model);
        Task<bool> ChangePasswordAsync(Guid id, string currentPassword, string newPassword);
    }
}