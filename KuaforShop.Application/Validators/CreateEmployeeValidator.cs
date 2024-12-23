using FluentValidation;
using KuaforShop.Application.DTOs.EmployeeDTOs;

namespace KuaforShop.Application.Validators
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDTO>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır")
                .MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olabilir");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim boş olamaz")
                .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olabilir");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyisim boş olamaz")
                .MaximumLength(50).WithMessage("Soyisim en fazla 50 karakter olabilir");

            RuleFor(x => x.SaloonId)
                .NotEmpty().WithMessage("Salon seçimi zorunludur");

            RuleFor(x => x.BeginTime)
                .NotEmpty().WithMessage("Başlangıç saati boş olamaz");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("Bitiş saati boş olamaz")
                .Must((model, endTime) => endTime > model.BeginTime)
                .WithMessage("Bitiş saati başlangıç saatinden sonra olmalıdır");

            RuleFor(x => x.Expertise)
                .NotEmpty().WithMessage("En az bir uzmanlık alanı seçilmelidir");
        }
    }
}