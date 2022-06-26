using FluentValidation;
using solarLab.Contracts.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Validators.Identity
{
    /// <summary>
    /// Валидатор для модели смены пароля
    /// </summary>
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(_ => _.Email)
                .NotEmpty()
                .WithMessage("Email не может быть пустым")
                .EmailAddress()
                .WithMessage("Email не является действительным");

            RuleFor(_ => _.OldPassword)
                .NotEmpty()
                .WithMessage("Пароль не может быть пустым");

            RuleFor(_ => _.NewPassword)
                .NotEmpty()
                .WithMessage("Новый пароль не может быть пустым");

            RuleFor(_ => _.ConfirmNewPassword)
                .Equal(_ => _.NewPassword)
                .WithMessage("Пароли не совпадают");

            RuleFor(_ => _.NewPassword)
                .NotEqual(_ => _.OldPassword)
                .WithMessage("Старый и новый пароли не могут быть одинаковыми");
        }
    }
}
