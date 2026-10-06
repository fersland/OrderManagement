using Application.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreatePetsValidator : AbstractValidator<CreatePetsDto>
    {
        public CreatePetsValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(100).WithMessage("El nombre solo permite 100 caracteres.");

            RuleFor(p => p.Colour)
                .MaximumLength(50).WithMessage("Este campo permite solo 50 caracteres");
        }
    }
}
