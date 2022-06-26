using FluentValidation;
using solarLab.Contracts.Identity;
using System;

namespace solarLab.Contracts.Validators.Identity
{
    /// <summary>
    /// Валидатор для модели регистрации
    /// </summary>
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Логин не может быть пустым");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Пароль не может быть пустым");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Имя не может быть пустым")
                .Matches("^[a-zA-Zа-яА-Я ]*$")
                .WithMessage("Имя может содержать только буквы");

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage("Неверное значение");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email не может быть пустым")
                .EmailAddress()
                .WithMessage("Email не является действительным");

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Today)
                .WithMessage("Дата рождения не может быть датой в будущем");

            RuleFor(_ => _.PhoneNumber)
                .Matches("[0-9]{11}")
                .When(x=>!string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("Неверный формат номера");

            RuleFor(_ => _.ConfirmPassword)
                .Equal(_ => _.Password)
                .WithMessage("Пароли не совпадают");

            RuleFor(_ => _.Avatar)
                .Length(0, 1048576)
                .WithMessage("Превышен размер файла. Максимальный размер - 1МБ");
        }
    }
}
