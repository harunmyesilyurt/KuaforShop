using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Application.DTOs.EmployeeDTOs
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Sex { get; set; }
        public Guid SaloonId { get; set; }
        public enmRoles Role { get; set; }
        public TimeOnly BeginTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public enmWorkDays WorkDays { get; set; }
    }
}