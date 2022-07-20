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
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Aplicacion.ServicesImpl
{
    public class BrandAppService : IBrandAppService
    {
        private readonly IGenericRepository<Brand> brandRepository;
        private readonly IValidator<CreateBrandDto> validator;
        private readonly IMapper mapper;

        public BrandAppService(IGenericRepository<Brand> brandRepository, 
            IValidator<CreateBrandDto> validator,
            IMapper mapper)
        {
            this.brandRepository = brandRepository;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<ResultPaginationBrand<BrandDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc")
        {

            var query = brandRepository.GetQueryable();

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


            //var result = await brandRepository.GetListAsync(limit, offset);
            var resultQuery = query.Select(b => new BrandDto()
            {
                Code = b.Code,
                Description = b.Description,
                CreationDate = DateTime.Now
            });
            //var resultDto=new List<BrandDto>();
            //foreach (var item in result)
            //{
            //    var brandDto = new BrandDto();
            //    brandDto.Code= item.Code;
            //    brandDto.Description= item.Description;
            //    brandDto.CreationDate= item.CreationDate;
            //    resultDto.Add(brandDto);
            //}
            //return resultDto;
            var item = await resultQuery.ToListAsync();
            var result = new ResultPaginationBrand<BrandDto>();
            result.Total = total;
            result.Items = item;

            return result;

        }
        public async Task<BrandDto> GetAsync(string code)
        {
            var entity = await brandRepository.GetAsync(code);
            if (entity == null)
            {
                throw new NotFoundException($"La marca con id -> {code} no existe");
            }
            return new BrandDto { 
                Code = entity.Code, 
                Description = entity.Description,
                CreationDate= entity.CreationDate
            };
        }
        public async Task CreateAsync(CreateBrandDto brandDto)
        {
            //var result = await validator.ValidateAsync(brandDto, options =>
            //{
            //    options.ThrowOnFailures();
            //});

            //var result = await validator.ValidateAsync(brandDto);
            //if (!result.IsValid)
            //{
            //    throw new Exception("Datos incorrectos");
            //}

            await validator.ValidateAndThrowAsync(brandDto);

            //var brand = new Brand()
            //{
            //    Code = brandDto.Code,
            //    Description= brandDto.Description,
            //    CreationDate = DateTime.Now,
            //};

            var brand = mapper.Map<Brand>(brandDto);
            brand.CreationDate = DateTime.Now;
            await brandRepository.CreateAsync(brand);          
        }

        public async Task UpdateAsync(string code, CreateBrandDto brandDto)
        {
            var brand = await brandRepository.GetAsync(code);
            if (brand == null)
            {
                throw new NotFoundException($"La marca con id -> {code} no puede ser actualizada");
            }
            brand.Description = brandDto.Description;
            brand.ModifiedDate = DateTime.Now;
            await brandRepository.UpdateAsync(brand);        
        }

        public async Task DeleteAsync(string code)
        {
            var entity = await brandRepository.GetAsync(code);
            if (entity == null)
            {
                throw new NotFoundException($"La marca con id -> {code} no existe");
            }
            entity.IsDelete = true;
            entity.ModifiedDate= DateTime.Now;
            await brandRepository.DeleteAsync(entity);
        }
    }
}
