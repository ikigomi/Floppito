using FluentValidation;
using solarLab.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Validators.User
{
    /// <summary>
    /// Валидатор для модели пользователя
    /// </summary>
    public class UserDtoValidator:AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage("Логин не можеть быть пустым");

            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("Имя не может быть пустым")
                .Matches("^[a-zA-Zа-яА-Я ]*$")
                .WithMessage("Имя может содержать только буквы");

            RuleFor(u => u.Gender)
                .NotEmpty()
                .WithMessage("Пол не может быть пустым");

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email не может быть пустым")
                .EmailAddress()
                .WithMessage("Email не является действительным");

        }
    }
}
