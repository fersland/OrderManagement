using Application.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateDeveloperValidator : AbstractValidator<UpdateDeveloperDto>
    {
        public UpdateDeveloperValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(100).WithMessage("El nombre solo permite 100 caracteres maximo.");
            RuleFor(x => x.Age)
                .NotEmpty().WithMessage("La edad es requerida.");
        }
    }
}
