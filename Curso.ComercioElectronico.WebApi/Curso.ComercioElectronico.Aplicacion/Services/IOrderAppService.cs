using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IOrderAppService
    {
        Task<ICollection<OrderDto>> GetAllAsync();
        Task<OrderDto> GetAsync(Guid Id);
        Task CreateAsync(CreateOrderDto orderlDto);
        Task UpdateAsync(Guid Id, CreateOrderDto orderDto);
        Task DeleteAsync(Guid Id);
    }

}
