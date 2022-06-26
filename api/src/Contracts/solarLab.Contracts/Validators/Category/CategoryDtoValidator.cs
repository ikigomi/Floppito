using FluentValidation;
using solarLab.Contracts.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Validators.Category
{
    /// <summary>
    /// Валидатор для модели поста
    /// </summary>
    public class CategoryDtoValidator:AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Название не может быть пустым")
                .Matches("^[a-zA-Zа-яА-Я ]*$")
                .WithMessage("Название может содержать только буквы");

        }
    }
}
