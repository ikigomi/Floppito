using FluentValidation;
using solarLab.Contracts.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Validators.Comment
{
    /// <summary>
    /// Валидатор для модели комментария
    /// </summary>
    public class CommentDtoValidator: AbstractValidator<CommentDto>
    {
        public CommentDtoValidator()
        {
            RuleFor(c => c.CommentBody)
                .NotEmpty()
                .WithMessage("Тело комментария не может быть пустым");

            RuleFor(c => c.PostId)
                .NotEmpty()
                .WithMessage("Идентификатор поста не может быть пустым");

            RuleFor(c => c.Author.Id)
                .NotEmpty()
                .WithMessage("Идентификатор автора не может быть пустым");

        }
    }
}
