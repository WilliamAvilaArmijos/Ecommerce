using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Exceptions;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Dominio.Entities;
using Curso.ComercioElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Aplicacion.ServicesImpl
{
    public class ProductTypeAppService : IProductTypeAppService
    {
        private readonly IGenericRepository<ProductType> productTypeRepository;
        private readonly IMapper mapper;

        public ProductTypeAppService(IGenericRepository<ProductType> productTypeRepository, IMapper mapper)
        {
            this.productTypeRepository = productTypeRepository;
            this.mapper = mapper;
        }
        public async Task<ResultPaginationProductType<ProductTypeDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc")
        {
            var query = productTypeRepository.GetQueryable();

            //FILTRAR
            query = query.Where(x => x.IsDelete == false);

            //BUSQUEDA

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(
                    x => x.Description.ToUpper().Contains(search)
                    || x.Code.ToUpper().StartsWith(search)
                    );
            }

            //1. TOTAL
            var total = await query.CountAsync();

            //2. PAGINACION
            query = query.Skip(offset).Take(limit);

            //3. ORDENAR
            if (!string.IsNullOrEmpty(sort))
            {
                //Soportar Name
                // sort => name or price other throw wxception
                switch (sort.ToUpper())
                {
                    case "CODE":
                        query = query.OrderBy(x => x.Code);
                        break;
                    case "DESCRIPTION":
                        query = query.OrderBy(x => x.Description);
                        break;
                    default:
                        throw new ArgumentException($"El parametro {sort} no es valido");
                }
            }
            var resultQuery = query.Select(b => new ProductTypeDto()
            {
                Code = b.Code,
                Description = b.Description,
                CreationDate = DateTime.Now
            });
            var item = await resultQuery.ToListAsync();
            var result = new ResultPaginationProductType<ProductTypeDto>();
            result.Total = total;
            result.Items = item;

            return result;
        }
        public async Task<ProductTypeDto> GetAsync(string code)
        {
            var entity = await productTypeRepository.GetAsync(code);
            if (entity == null)
            {
                throw new NotFoundException($"El tipo de producto con id  -> {code} no existe");
            }
            return new ProductTypeDto
            {
                Code = entity.Code,
                Description = entity.Description,
                CreationDate = entity.CreationDate
            };
        }
        public async Task CreateAsync(CreateProductTypeDto productTypeDto)
        {
            //var productType = new ProductType();
            //productType.Code = productTypeDto.Code;
            //productType.Description = productTypeDto.Description;
            //productType.CreationDate = DateTime.Now;

            var productType = mapper.Map<ProductType>(productTypeDto);
            productType.CreationDate = DateTime.Now;
            await productTypeRepository.CreateAsync(productType);
        }
        public async Task UpdateAsync(string code, CreateProductTypeDto productTypeDto)
        {
            var productType = await productTypeRepository.GetAsync(code);
            if (productType == null)
            {
                throw new NotFoundException($"El tipo de producto con id -> {code} no puede ser actualizada");
            }
            productType.Description = productTypeDto.Description;
            productType.ModifiedDate = DateTime.Now;
            await productTypeRepository.UpdateAsync(productType);
        }
        public async Task DeleteAsync(string code)
        {
            var entity = await productTypeRepository.GetAsync(code);
            if (entity == null)
            {
                throw new NotFoundException($"El tipo de producto con id -> {code} no existe");
            }
            entity.IsDelete = true;
            entity.ModifiedDate = DateTime.Now;
            await productTypeRepository.DeleteAsync(entity);
        }
    }
}
