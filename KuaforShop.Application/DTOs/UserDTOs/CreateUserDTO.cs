using KuaforShop.Core.Enumertaions;
using System.ComponentModel.DataAnnotations;

namespace KuaforShop.Application.DTOs.UserDTOs
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfirmPassword { get; set; }


        public bool Sex { get; set; }

        public enmRoles Role { get; set; }
    }
}