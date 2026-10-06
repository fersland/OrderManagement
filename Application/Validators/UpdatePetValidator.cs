using Application.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdatePetValidator : AbstractValidator<UpdatePetsDto>
    {
        public UpdatePetValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .MaximumLength(100).WithMessage("El nombre solo permite 100 caracteres");

            RuleFor(p => p.Colour)
                .MaximumLength(40).WithMessage("Este campo solo permite 40 caracteres");
        }
    }
}
