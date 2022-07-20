using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface ICustomerAppService
    {
        Task<ResultPaginationCustomer<CustomerDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc");
        Task<CustomerDto> GetAsync(string code);
        Task CreateAsync(CreateCustomerDto customerDto);
        Task UpdateAsync(string code, CreateCustomerDto customerDto);
        Task DeleteAsync(string code);
    }

    public class ResultPaginationCustomer<T>
    {
        public int Total { get; set; }
        public ICollection<T> Items { get; set; } = new List<T>();
    }
}
