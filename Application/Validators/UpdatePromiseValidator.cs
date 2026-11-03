using Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdatePromiseValidator : AbstractValidator<UpdatePromiseDto>
    {
        public UpdatePromiseValidator() {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Este campo es obligatorio.").MaximumLength(40).WithMessage("Este campo solo permite 40 caracteres.");
            RuleFor(x => x.Age).NotEmpty().WithMessage("Este campo es obligatorio");        
        }
    }
}
