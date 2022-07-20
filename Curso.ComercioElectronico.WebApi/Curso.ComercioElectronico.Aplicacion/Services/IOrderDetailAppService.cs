using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IOrderDetailAppService
    {
        Task<ICollection<OrderDetailDto>> GetAllAsync();
        Task<OrderDetailDto> GetAsync(Guid Id);
        Task CreateAsync(CreateOrderDetailDto orderDetailDto);
        Task UpdateAsync(Guid Id, CreateOrderDetailDto orderDetailDto);
        Task DeleteAsync(Guid Id);
    }

}
