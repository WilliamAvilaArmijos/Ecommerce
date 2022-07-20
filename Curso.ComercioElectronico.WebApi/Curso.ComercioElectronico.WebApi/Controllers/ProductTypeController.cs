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
    public class ProductTypeController : ControllerBase, IProductTypeAppService
    {
        private readonly IProductTypeAppService productTypeAppService;

        public ProductTypeController(IProductTypeAppService productTypeAppService)
        {
            this.productTypeAppService = productTypeAppService;
        }
        [HttpGet]
        public async Task<ResultPaginationProductType<ProductTypeDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc")
        {
            return await productTypeAppService.GetListAsync(search, offset, limit, sort, order);
        }

        [HttpGet("{code}")]
        public async Task<ProductTypeDto> GetAsync(string code)
        {
            return await productTypeAppService.GetAsync(code);
        }

        [HttpPost]
        public async Task CreateAsync(CreateProductTypeDto productTypeDto)
        {
            await productTypeAppService.CreateAsync(productTypeDto);
        }
        [HttpPut]
        public async Task UpdateAsync(string code, CreateProductTypeDto productTypeDto)
        {
            await productTypeAppService.UpdateAsync(code,productTypeDto);
        }
        [HttpDelete]
        public async Task DeleteAsync(string code)
        {
            await productTypeAppService.DeleteAsync(code);
        }
    }
}
