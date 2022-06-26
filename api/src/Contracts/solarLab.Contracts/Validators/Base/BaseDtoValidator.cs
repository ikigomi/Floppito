using FluentValidation;
using solarLab.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Validators.Base
{
    /// <summary>
    /// Валидатор для базовой модели
    /// </summary>
    public class BaseDtoValidator:AbstractValidator<BaseDto>
    {
        public BaseDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Идентификатор не может быть пустым");
        }
    }
}
