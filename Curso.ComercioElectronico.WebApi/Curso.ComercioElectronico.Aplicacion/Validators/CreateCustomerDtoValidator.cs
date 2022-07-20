using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using FluentValidation;

namespace Curso.ComercioElectronico.Aplicacion.Validators
{
    public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .NotNull()
                .MaximumLength(10)
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Solo soporta números y letras");

        }
    }
}
