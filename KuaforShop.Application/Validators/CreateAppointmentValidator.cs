using FluentValidation;
using KuaforShop.Application.DTOs.AppointmentDTOs;

namespace KuaforShop.Application.Validators
{
    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentDTO>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı seçimi zorunludur");

            RuleFor(x => x.ServiceId)
                .NotEmpty().WithMessage("Hizmet seçimi zorunludur");

            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("Çalışan seçimi zorunludur");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Randevu tarihi zorunludur")
                .Must(date => date > DateTime.Now)
                .WithMessage("Randevu tarihi gelecekte bir tarih olmalıdır");
        }
    }
}