﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class BrandDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
