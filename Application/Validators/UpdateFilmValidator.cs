using Application.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateFilmValidator : AbstractValidator<UpdateFilmDto>
    {
        public UpdateFilmValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El titulo es requerido.")
                .MaximumLength(100).WithMessage("El titulo solo permite 100 caracteres maximo.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripcion solo permite 500 caracteres maximo.");
                
        }
    }
}
