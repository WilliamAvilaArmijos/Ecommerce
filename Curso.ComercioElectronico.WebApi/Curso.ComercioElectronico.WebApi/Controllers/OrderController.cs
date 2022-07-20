using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase, IOrderAppService
    {
        private readonly IOrderAppService service;

        public OrderController(IOrderAppService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<ICollection<OrderDto>> GetAllAsync()
        {
            return await service.GetAllAsync();
        }

        [HttpGet("{Id}")]
        public async Task<OrderDto> GetAsync(Guid Id)
        {
            return await service.GetAsync(Id);
        }

        [HttpPost]
        public async Task CreateAsync(CreateOrderDto orderDto)
        {
            await service.CreateAsync(orderDto);
        }
        [HttpPut]
        public async Task UpdateAsync(Guid Id, CreateOrderDto orderDto)
        {
            await service.UpdateAsync(Id, orderDto);
        }
        [HttpDelete]
        public async Task DeleteAsync(Guid Id)
        {
            await service.DeleteAsync(Id);
        }
    }
}
