using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using FluentValidation;

namespace Curso.ComercioElectronico.Aplicacion
{
    public class CreateBrandDtoValidator : AbstractValidator<CreateBrandDto>
    {
        public CreateBrandDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .NotNull()
                .MaximumLength(4)
                .Matches("^[a-zA-Z0-9-]*$")
                .WithMessage("Solo soporta números y letras");
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(254)
                .WithMessage("La descripcion no puede ser nula o vacia");


            //Regla personalizada
            //RuleFor(x => x.Description)
            //    .Must(a => ValidarDescripcion(a));

        }

        //public bool ValidarDescripcion(string validarDescipcion)
        //{
        //    if(validarDescipcion.Length>3)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}
