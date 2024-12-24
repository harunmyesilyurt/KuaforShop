using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Models
{
    public class SaloonDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public TimeOnly OpenTime { get; set; }
        public TimeOnly CloseTime { get; set; }
        public enmWorkDays WorkDays { get; set; }
        public enmSaloonType SaloonType { get; set; }
        public List<EmployeeViewModel> Employees { get; set; }
        public List<ServiceViewModel> Services { get; set; }
    }

    public class EmployeeViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public List<string> Expertise { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class ServiceViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public enmDurationType DurationType { get; set; }
    }
}