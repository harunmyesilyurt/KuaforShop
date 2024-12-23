using FluentValidation;
using KuaforShop.Application.DTOs.ServiceDTOs;

namespace KuaforShop.Application.Validators
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceDTO>
    {
        public CreateServiceValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hizmet adı boş olamaz")
                .MinimumLength(2).WithMessage("Hizmet adı en az 2 karakter olmalıdır")
                .MaximumLength(100).WithMessage("Hizmet adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır");

            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage("Süre 0'dan büyük olmalıdır");

            RuleFor(x => x.SaloonId)
                .NotEmpty().WithMessage("Salon seçimi zorunludur");
        }
    }
}