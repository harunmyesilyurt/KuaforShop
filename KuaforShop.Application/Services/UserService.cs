using AutoMapper;
using KuaforShop.Application.DTOs.UserDTOs;
using KuaforShop.Core.Entities;
using KuaforShop.Core.Enumertaions;
using KuaforShop.Persistence.Repositories.User;
using KuaforShop;
using Microsoft.EntityFrameworkCore; // Web.Models olarak değiştirdik

namespace KuaforShop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(CreateUserDTO createUserDTO)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(createUserDTO.Username);
            if (existingUser != null)
                return false;

            var user = _mapper.Map<Users>(createUserDTO);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _userRepository.RemoveAsync(id);
            await _userRepository.SaveAsync();
            return result;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAll().ToListAsync();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> GetByRoleAsync(enmRoles role)
        {
            var users = await _userRepository.GetByRoleAsync(role);
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<bool> UpdateAsync(Guid id, CreateUserDTO updateUserDTO)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            // Kullanıcı adı değiştirilmek isteniyorsa ve yeni kullanıcı adı başkası tarafından kullanılıyorsa
            if (user.Username != updateUserDTO.Username)
            {
                var existingUser = await _userRepository.GetByUsernameAsync(updateUserDTO.Username);
                if (existingUser != null)
                    return false;
            }

            user.Username = updateUserDTO.Username;
            user.Sex = updateUserDTO.Sex;

            if (!string.IsNullOrEmpty(updateUserDTO.Password))
            {
                user.Password = updateUserDTO.Password; // Gerçek uygulamada hash'lenmiş olmalı
            }

            _userRepository.Update(user);
            await _userRepository.SaveAsync();
            return true;
        }

        public async Task<UserDTO> ValidateUserAsync(LoginDTO loginDTO)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDTO.Username);
            if (user == null || user.Password != loginDTO.Password) // Gerçek uygulamada şifre hash'lenmiş olmalı
                return null;

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _userRepository.GetAll().CountAsync();
        }

        //public async Task<bool> UpdateProfileAsync(Guid id, UserProfileViewModel model)
        //{
        //    var user = await _userRepository.GetByIdAsync(id);
        //    if (user == null)
        //        return false;

        //    // Kullanıcı adı değiştirilmek isteniyorsa ve yeni kullanıcı adı başkası tarafından kullanılıyorsa
        //    if (user.Username != model.Username)
        //    {
        //        var existingUser = await _userRepository.GetByUsernameAsync(model.Username);
        //        if (existingUser != null)
        //            return false;
        //    }

        //    user.Username = model.Username;
        //    user.Email = model.Email;
        //    user.Phone = model.Phone;
        //    user.Sex = model.Sex;
        //    user.NotificationEnabled = model.NotificationEnabled;
        //    user.PreferredSaloonId = model.PreferredSaloonId;

        //    _userRepository.Update(user);
        //    await _userRepository.SaveAsync();
        //    return true;
        //}

        public async Task<bool> ChangePasswordAsync(Guid id, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null || user.Password != currentPassword)
                return false;

            user.Password = newPassword;
            _userRepository.Update(user);
            await _userRepository.SaveAsync();
            return true;
        }
    }
}