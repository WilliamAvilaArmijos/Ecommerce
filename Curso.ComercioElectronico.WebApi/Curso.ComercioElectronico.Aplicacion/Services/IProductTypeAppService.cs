using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IProductTypeAppService
    {
        //Task<ICollection<ProductType>> GetAsync();
        //Task<ICollection<ProductTypeDto>> GetAllAsync();

        Task<ResultPaginationProductType<ProductTypeDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc");
        Task<ProductTypeDto> GetAsync(string code);
        Task CreateAsync(CreateProductTypeDto productTypeDto);
        Task UpdateAsync(string code, CreateProductTypeDto productTypeDto);
        Task DeleteAsync(string code);

    }
    public class ResultPaginationProductType<T>
    {
        public int Total { get; set; }
        public ICollection<T> Items { get; set; } = new List<T>();
    }
}
