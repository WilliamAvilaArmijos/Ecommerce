using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Dominio.Entities;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public decimal SubTotal { get; set; } = 0;
        public decimal Total { get; set; } = 0;
        public virtual List<OrderDetailDto> orderDetails { get; set; }
    }
}
