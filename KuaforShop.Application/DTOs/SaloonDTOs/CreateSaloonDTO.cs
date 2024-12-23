using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Application.DTOs.SaloonDTOs
{
    public class CreateSaloonDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public TimeOnly OpenTime { get; set; }
        public TimeOnly CloseTime { get; set; }
        public enmWorkDays WorkDays { get; set; }
        public enmSaloonType SaloonType { get; set; }
        public string MyProperty { get; set; }
    }
}