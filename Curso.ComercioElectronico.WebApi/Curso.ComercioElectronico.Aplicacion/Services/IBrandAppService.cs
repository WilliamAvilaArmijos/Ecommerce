using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IBrandAppService
    {
        //Task<ICollection<Brand>> GetAsync();
        Task<ResultPaginationBrand<BrandDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc");
        //Task<ICollection<BrandDto>> GetAllAsync();
        Task<BrandDto> GetAsync(string code);
        Task CreateAsync(CreateBrandDto brandDto);
        Task UpdateAsync(string code, CreateBrandDto brandDto);
        Task DeleteAsync(string code);
    }

    public class ResultPaginationBrand<T>
    {
        public int Total { get; set; }
        public ICollection<T> Items { get; set; } = new List<T>();
    }
}
