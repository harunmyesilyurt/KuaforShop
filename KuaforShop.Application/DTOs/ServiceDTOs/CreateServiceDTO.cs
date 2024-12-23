using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Application.DTOs.ServiceDTOs
{
    public class CreateServiceDTO
    {
        public Guid SaloonId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public enmDurationType DurationType { get; set; }
    }
}