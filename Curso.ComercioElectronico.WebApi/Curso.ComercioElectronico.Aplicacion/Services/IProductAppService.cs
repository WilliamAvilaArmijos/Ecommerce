using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IProductAppService
    {
        Task<ResultPagination<ProductDto>> GetListAsync(string? search="" ,int offset=0, int limit=10, string sort="Name", string order="asc" );
        Task<ProductDto> GetAsync(Guid Id);
        Task CreateAsync(CreateProductDto productDto);
        Task UpdateAsync(Guid Id, CreateProductDto productDto);
        Task DeleteAsync(Guid Id);

    }

    public class ResultPagination<T>
    {
        public int Total { get; set; }
        public ICollection<T> Items { get; set; } = new List<T>();
    }
}
