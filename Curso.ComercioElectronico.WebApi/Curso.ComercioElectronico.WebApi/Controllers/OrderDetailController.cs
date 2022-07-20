using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase, IOrderDetailAppService
    {
        private readonly IOrderDetailAppService service;

        public OrderDetailController(IOrderDetailAppService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<ICollection<OrderDetailDto>> GetAllAsync()
        {
            return await service.GetAllAsync();
        }

        [HttpGet("{Id}")]
        public async Task<OrderDetailDto> GetAsync(Guid Id)
        {
            return await service.GetAsync(Id);
        }

        [HttpPost]
        public async Task CreateAsync(CreateOrderDetailDto orderDetailDto)
        {
            await service.CreateAsync(orderDetailDto);
        }
        [HttpPut]
        public async Task UpdateAsync(Guid Id, CreateOrderDetailDto orderDetailDto)
        {
            await service.UpdateAsync(Id, orderDetailDto);
        }
        [HttpDelete]
        public async Task DeleteAsync(Guid Id)
        {
            await service.DeleteAsync(Id);
        }
    }
}
