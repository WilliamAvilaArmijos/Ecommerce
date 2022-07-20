using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Dominio.Entities;
using Curso.ComercioElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructura.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly ComercioElectronicoDbContext context;

        public ProductTypeRepository(ComercioElectronicoDbContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<ProductType>> GetAsync()
        {
            return await context.ProductTypes.ToListAsync();
        }

        public async Task<ProductType> GetAsync(string code)
        {
         
            return await context.ProductTypes.FindAsync();
        }
    }
}
