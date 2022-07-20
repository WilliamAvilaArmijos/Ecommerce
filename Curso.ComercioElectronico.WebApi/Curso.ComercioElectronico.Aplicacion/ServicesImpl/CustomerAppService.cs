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
    public class CustomerAppService: ICustomerAppService
    {
        private readonly IGenericRepository<Customer> repository;
        private readonly IValidator<CreateCustomerDto> validator;
        private readonly IMapper mapper;

        public CustomerAppService(IGenericRepository<Customer> repository,
            IValidator<CreateCustomerDto> validator,
            IMapper mapper)
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<ResultPaginationCustomer<CustomerDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Name", string order = "asc")
        {

            var query = repository.GetQueryable();

            //FILTRAR
            query = query.Where(x => x.IsDelete == false);

            //BUSQUEDA

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(
                    x => x.Name.ToUpper().Contains(search)
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
                    case "NAME":
                        query = query.OrderBy(x => x.Name);
                        break;
                    default:
                        throw new ArgumentException($"El parametro {sort} no es valido");

                }
            }


            //var result = await brandRepository.GetListAsync(limit, offset);
            var resultQuery = query.Select(b => new CustomerDto()
            {
                Code = b.Code,
                Name = b.Name,
                Address = b.Address,
                Email = b.Email,
                Country = b.Country,
                CreationDate = DateTime.Now
            });
 
            var item = await resultQuery.ToListAsync();
            var result = new ResultPaginationCustomer<CustomerDto>();
            result.Total = total;
            result.Items = item;

            return result;

        }
        public async Task<CustomerDto> GetAsync(string code)
        {
            var entity = await repository.GetAsync(code);
            if (entity == null)
            {
                throw new NotFoundException($"El cliente -> {code} no existe");
            }
            return new CustomerDto
            {
                Code = entity.Code,
                Name = entity.Name,
                Address = entity.Address,
                Country = entity.Country,
                Email = entity.Email,
                CreationDate = entity.CreationDate
            };
        }
        public async Task CreateAsync(CreateCustomerDto customerDto)
        {
            await validator.ValidateAndThrowAsync(customerDto);

            var entity = mapper.Map<Customer>(customerDto);
            entity.CreationDate = DateTime.Now;
            await repository.CreateAsync(entity);
        }

        public async Task UpdateAsync(string code, CreateCustomerDto customerDto)
        {
            var entity = await repository.GetAsync(code);
            if (entity == null)
            {
                throw new NotFoundException($"El cliente -> {code} no puede ser actualizada");
            }
            entity.Name = customerDto.Name;
            entity.Address = customerDto.Address;
            entity.Country = customerDto.Country;
            entity.Email = customerDto.Email;
            entity.ModifiedDate = DateTime.Now;
            await repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string code)
        {
            var entity = await repository.GetAsync(code);
            if (entity == null)
            {
                throw new NotFoundException($"El cliente -> {code} no existe");
            }
            entity.IsDelete = true;
            entity.ModifiedDate = DateTime.Now;
            await repository.DeleteAsync(entity);
        }
    }
}
