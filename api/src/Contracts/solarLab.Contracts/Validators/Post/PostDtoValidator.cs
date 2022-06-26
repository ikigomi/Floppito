using FluentValidation;
using solarLab.Contracts.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Validators.Post
{
    /// <summary>
    /// Валидатор для модели поста
    /// </summary>
    public class PostDtoValidator:AbstractValidator<PostDto>
    {
        public PostDtoValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Идентификатор категории не может быть пустым");

            RuleFor(x => x.Author.Id)
                .NotEmpty()
                .WithMessage("Идентификатор автора не может быть пустым");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Название не может быть пустым");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Описание не может быть пустым")
                .When(x=>x.Images?.Count==0 && string.IsNullOrWhiteSpace(x.Description))
                .WithMessage("Описание или картинки должны присутствовать");
        }
    }
}
