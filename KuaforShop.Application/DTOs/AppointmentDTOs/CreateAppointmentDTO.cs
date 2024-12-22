namespace KuaforShop.Application.DTOs.AppointmentDTOs
{
    public class CreateAppointmentDTO
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public double Price { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime Date { get; set; }
    }
}