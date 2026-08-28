using Application.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateCustomerValidator : AbstractValidator<CreateDtoCustomer>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Ci)
                .NotEmpty().WithMessage("La cedula es requerida.")
                .Length(10, 13).WithMessage("La cedula o RUC debe contener minimo 10 a 13 numeros");

            RuleFor(x => x.Fname)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .MaximumLength(40).WithMessage("Este campo solo permite 40 caracteres maximo.");
            RuleFor(x => x.Lname)
                .MaximumLength(40).WithMessage("Este campo permite hasta 40 caracteres maximo.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Este campo es requerido")
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("El formato del email es incorrecto.");
        }
    }
}
