using FluentValidation;
using KuaforShop.Application.DTOs.SaloonDTOs;

namespace KuaforShop.Application.Validators
{
    public class CreateSaloonValidator : AbstractValidator<CreateSaloonDTO>
    {
        public CreateSaloonValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Salon adı boş olamaz")
                .MinimumLength(3).WithMessage("Salon adı en az 3 karakter olmalıdır")
                .MaximumLength(100).WithMessage("Salon adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres boş olamaz")
                .MaximumLength(500).WithMessage("Adres en fazla 500 karakter olabilir");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz")
                .Matches(@"^[0-9\-\+]{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz");

            RuleFor(x => x.OpenTime)
                .NotEmpty().WithMessage("Açılış saati boş olamaz");

            RuleFor(x => x.CloseTime)
                .NotEmpty().WithMessage("Kapanış saati boş olamaz")
                .Must((model, closeTime) => closeTime > model.OpenTime)
                .WithMessage("Kapanış saati açılış saatinden sonra olmalıdır");
        }
    }
}