using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Dominio.Entities.Base;

namespace Curso.ComercioElectronico.Dominio.Entities
{
    public class Order: BaseBusinessEntity
    {
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public virtual List<OrderDetail> orderDetails { get; set; }
    }
}
