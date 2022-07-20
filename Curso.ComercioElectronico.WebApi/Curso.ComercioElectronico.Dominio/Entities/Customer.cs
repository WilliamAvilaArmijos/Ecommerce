using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Dominio.Entities.Base;

namespace Curso.ComercioElectronico.Dominio.Entities
{
    public class Customer : BaseCatologEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
    }
}
