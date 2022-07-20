using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase, IProductAppService
    {
        private readonly IProductAppService productAppService;

        public ProductController(IProductAppService productAppService)
        {
            this.productAppService = productAppService;
        }
        [HttpGet]

        public async Task<ResultPagination<ProductDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Name", string order = "asc")
        {
            return await productAppService.GetListAsync(search,offset,limit,sort,order);
        }

        [HttpGet("{Id}")]
        public async Task<ProductDto> GetAsync(Guid Id)
        {
            return await productAppService.GetAsync(Id);
        }
        [HttpPost]
        public async Task CreateAsync(CreateProductDto productDto)
        {
            await productAppService.CreateAsync(productDto);
        }
        [HttpPut]
        public async Task UpdateAsync(Guid Id, CreateProductDto productDto)
        {
            await productAppService.UpdateAsync(Id,productDto);
        }
        [HttpDelete]
        public async Task DeleteAsync(Guid Id)
        {
            await productAppService.DeleteAsync(Id);
        }
    }
}
