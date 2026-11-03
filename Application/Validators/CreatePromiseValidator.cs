using Application.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreatePromiseValidator : AbstractValidator<CreatePromiseDto>
    {
        public CreatePromiseValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Este campo es obligatorio.").MaximumLength(40).WithMessage("Este campo solo permite 40 caracteres.");
            RuleFor(x => x.Age).NotEmpty().WithMessage("Este campo es oblogatorio");
        }
    }
}
