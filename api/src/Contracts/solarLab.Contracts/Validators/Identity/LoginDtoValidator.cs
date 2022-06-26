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
    /// Валидатор для модели входа
    /// </summary>
    public class LoginDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email не может быть пустым")
                .EmailAddress()
                .WithMessage("Email не является действительным");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Пароль не может быть пустым");
        }
    }
}
