using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase, ICustomerAppService
    {
        private readonly ICustomerAppService service;

        public CustomerController(ICustomerAppService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<ResultPaginationCustomer<CustomerDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Name", string order = "asc")
        {
            return await service.GetListAsync(search, offset, limit, sort, order);
        }

        [HttpGet("{code}")]
        public async Task<CustomerDto> GetAsync(string code)
        {
            return await service.GetAsync(code);
        }

        [HttpPost]
        public async Task CreateAsync(CreateCustomerDto customerDto)
        {
            await service.CreateAsync(customerDto);
        }
        [HttpPut]
        public async Task UpdateAsync(string code, CreateCustomerDto customerDto)
        {
            await service.UpdateAsync(code, customerDto);
        }
        [HttpDelete]
        public async Task DeleteAsync(string code)
        {
            await service.DeleteAsync(code);
        }
    }
}
