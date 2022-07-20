using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Dominio.Entities;

namespace Curso.ComercioElectronico.Dominio.Repositories
{
    public interface IProductTypeRepository
    {
        Task<ICollection<ProductType>> GetAsync();
        Task<ProductType> GetAsync(string code);
    }
}
