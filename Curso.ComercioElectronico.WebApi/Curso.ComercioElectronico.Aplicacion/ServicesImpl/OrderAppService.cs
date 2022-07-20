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
    public class OrderAppService : IOrderAppService
    {
        private readonly IGenericRepository<Order> repository;
        private readonly IMapper mapper;
        private readonly IGenericRepository<OrderDetail> repositoryOrderDetail;

        public OrderAppService(IGenericRepository<Order> repository,
            IMapper mapper,
            IGenericRepository<OrderDetail> repositoryOrderDetail)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.repositoryOrderDetail = repositoryOrderDetail;
        }

        public async Task CreateAsync(CreateOrderDto orderlDto)
        {
            var entity = mapper.Map<Order>(orderlDto);
            entity.CreationDate = DateTime.Now;
            await repository.CreateAsync(entity);
        }

        public async Task DeleteAsync(Guid Id)
        {
            var entity = await repository.GetAsync(Id);
            if (entity == null)
            {
                throw new NotFoundException($"La orden -> {Id} no existe");
            }
            entity.IsDelete = true;
            entity.ModifiedDate = DateTime.Now;
            await repository.DeleteAsync(entity);
        }

        public async Task<ICollection<OrderDto>> GetAllAsync()
        {
            decimal total = 0;
            decimal totalOrden = 0;
            var consulta = repository.GetQueryable();
            var consulta2 = repositoryOrderDetail.GetQueryable();

            var items = await consulta2.Select(x => new OrderDetailDto()
            {
                Product=x.Product.Name,
                Price=x.Product.Price,
                Quantity=x.Quantity,
                Total= x.Product.Price * x.Quantity
            }).ToListAsync();
            foreach (var item in items)
            {
                total = consulta2.Sum(x =>( x.Product.Price * x.Quantity* (decimal)0.12));
                totalOrden = consulta2.Sum(a => a.Product.Price * a.Quantity);
            }
             
            var ordenes = await consulta.Select(c => new OrderDto()
            {
                Id = c.Id,
                Customer = c.CustomerId,
                SubTotal = totalOrden-total,
                Total = totalOrden,
                orderDetails = items,
            }).ToListAsync();
            
            return ordenes;
        }

        public async Task<OrderDto> GetAsync(Guid Id)
        {
            decimal total = 0;
            decimal totalOrden = 0;
            var consulta = repository.GetQueryable();
            consulta = consulta.Where(c => c.Id == Id);
            var consulta2 = repositoryOrderDetail.GetQueryable();
            var items = await consulta2.Select(x => new OrderDetailDto()
            {
                Product = x.Product.Name,
                Price = x.Product.Price,
                Quantity = x.Quantity,
                Total = x.Product.Price * x.Quantity
            }).ToListAsync();
            foreach (var item in items)
            {
                total = consulta2.Sum(x => (x.Product.Price * x.Quantity * (decimal)0.12));
                totalOrden = consulta2.Sum(a => a.Product.Price * a.Quantity);
            }

            var ordenes = await consulta.Select(c => new OrderDto()
            {
                Id = c.Id,
                Customer = c.CustomerId,
                SubTotal = totalOrden - total,
                Total = totalOrden,
                orderDetails = items,
            }).SingleOrDefaultAsync();

            return ordenes;
        }

        public async Task UpdateAsync(Guid Id, CreateOrderDto orderDto)
        {
            var order = await repository.GetAsync(Id);
            if (order == null)
            {
                throw new NotFoundException($"La orden -> {Id} no puede ser actualizado");
            }
            order.CustomerId = orderDto.CustomerId;
            order.ModifiedDate = DateTime.Now;
            await repository.UpdateAsync(order);
        }
    }
}
