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
    public class BrandController : ControllerBase, IBrandAppService
    {
        private readonly IBrandAppService brandAppService;

        public BrandController(IBrandAppService brandAppService)
        {
            this.brandAppService = brandAppService;
        }
        [HttpGet]
        public async Task<ResultPaginationBrand<BrandDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc")
        {
            return await brandAppService.GetListAsync(search, offset, limit, sort, order);
        }

        [HttpGet("{code}")]
        public async Task<BrandDto> GetAsync(string code)
        {
            return await brandAppService.GetAsync(code);
        }

        [HttpPost]
        public async Task CreateAsync(CreateBrandDto brandDto)
        {
            await brandAppService.CreateAsync(brandDto);
        }
        [HttpPut]
        public async Task UpdateAsync(string code, CreateBrandDto brandDto)
        {
            await brandAppService.UpdateAsync(code, brandDto);
        }
        [HttpDelete]
        public async Task DeleteAsync(string code)
        {
            await brandAppService.DeleteAsync(code);
        }
        
    }
}
