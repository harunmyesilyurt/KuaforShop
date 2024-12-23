using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Application.DTOs.ServiceDTOs
{
    public class ServiceDTO
    {
        public Guid Id { get; set; }
        public Guid SaloonId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public enmDurationType DurationType { get; set; }
    }
}