using Application.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateProductValidator : AbstractValidator<ProductCreateDto>
    {
        public CreateProductValidator() {
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripcion es requerida.")
                .MaximumLength(100).WithMessage("Este campo permite solo 100 caracteres.");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Este campo es requerido.");
            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage("Este campo es requerido.");

        }
    }
}
