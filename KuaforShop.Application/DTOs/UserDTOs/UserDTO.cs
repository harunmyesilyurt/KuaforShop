using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Application.DTOs.UserDTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Sex { get; set; }
        public bool NotificationEnabled { get; set; }
        public Guid? PreferredSaloonId { get; set; }
        public enmRoles Role { get; set; }
    }
}