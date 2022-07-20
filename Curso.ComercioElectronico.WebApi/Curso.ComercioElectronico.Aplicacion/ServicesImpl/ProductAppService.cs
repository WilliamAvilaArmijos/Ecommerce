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
    public class ProductAppService : IProductAppService
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IMapper mapper;

        public ProductAppService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ResultPagination<ProductDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Name", string order = "asc")
        {
            var query = productRepository.GetQueryable();

            //FILTRAR
            query = query.Where(x => x.IsDelete == false);

            //BUSQUEDA

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(
                    x=>x.Name.ToUpper().Contains(search)
                    );
            }

                //1. TOTAL
                var total= await query.CountAsync();

            //2. PAGINACION
            query = query.Skip(offset).Take(limit);

            //3. ORDENAR
            if (!string.IsNullOrEmpty(sort))
            {
                //Soportar Name
                // sort => name or price other throw wxception
                switch (sort.ToUpper())
                {
                    case "NAME":
                        query = query.OrderBy(x => x.Name);
                        break;
                    case "PRICE":
                        query = query.OrderBy(x=>x.Price);
                        break;
                    default:
                        throw new ArgumentException($"El parametro {sort} no es valido");

                }
            }
            
            var resultQuery = query.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description= x.Description,
                Price = x.Price,
                ProductType = x.ProductType.Description,
                Brand = x.Brand.Description
            });
            var item = await resultQuery.ToListAsync();
            var result = new ResultPagination<ProductDto>();
            result.Total = total;
            result.Items = item;

            return result;
        }

        public async Task<ProductDto> GetAsync(Guid Id)
        {
            var product = await productRepository.GetAsync(Id);
            if (product == null)
            {
                throw new NotFoundException($"El producto con id -> {Id} no existe");
            }
            var query = productRepository.GetQueryable();
            query = query.Where(x=>x.Id==Id);
            var resultQuery = query.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ProductType = x.ProductType.Description,
                Brand = x.Brand.Description
            });
            return await resultQuery.SingleOrDefaultAsync();
        }
        public async Task CreateAsync(CreateProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);
            product.Id= Guid.NewGuid();
            product.CreationDate = DateTime.Now;
            await productRepository.CreateAsync(product);
        }
        public async Task UpdateAsync(Guid Id, CreateProductDto productDto)
        {
            var product = await productRepository.GetAsync(Id);
            if (product == null)
            {
                throw new NotFoundException($"El producto con id -> {Id} no puede ser actualizado");
            }
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            product.ModifiedDate = DateTime.Now;
            await productRepository.UpdateAsync(product);
        }
        public async Task DeleteAsync(Guid Id)
        {
            var entity = await productRepository.GetAsync(Id);
            if (entity == null)
            {
                throw new NotFoundException($"El producto con id -> {Id} no existe");
            }
            entity.IsDelete = true;
            entity.ModifiedDate = DateTime.Now;
            await productRepository.DeleteAsync(entity);
        }
    }
}
