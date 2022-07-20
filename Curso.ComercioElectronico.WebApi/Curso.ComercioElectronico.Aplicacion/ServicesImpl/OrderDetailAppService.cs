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
    public class OrderDetailAppService : IOrderDetailAppService
    {
        private readonly IGenericRepository<OrderDetail> repository;
        private readonly IMapper mapper;

        public OrderDetailAppService(IGenericRepository<OrderDetail> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task CreateAsync(CreateOrderDetailDto orderDetailDto)
        {
            var consulta = new OrderDetail
            {
                OrderId = orderDetailDto.OrderId,
                ProductId = orderDetailDto.ProductId,
                Quantity = orderDetailDto.Quantity,
                Price = orderDetailDto.Price,
                Total = orderDetailDto.Quantity * orderDetailDto.Price,
            };
            await repository.CreateAsync(consulta);
        }

        public async Task DeleteAsync(Guid Id)
        {
            var entity = await repository.GetAsync(Id);
            if (entity == null)
            {
                throw new NotFoundException($"Los items -> {Id} no existe");
            }
            entity.IsDelete = true;
            entity.ModifiedDate = DateTime.Now;
            await repository.DeleteAsync(entity);
        }

        public async Task<ICollection<OrderDetailDto>> GetAllAsync()
        {
            var consulta = repository.GetQueryable();
            var ordenes = await consulta.Select(c => new OrderDetailDto
            {
                
                Product = c.Product.Name,
                Quantity = c.Quantity,
                Price = c.Product.Price,
                Total = c.Product.Price* c.Quantity,
            }).ToListAsync();
            return ordenes;
        }

        public async Task<OrderDetailDto> GetAsync(Guid Id)
        {
            var consulta = repository.GetQueryable();
            consulta = consulta.Where(c => c.Id == Id);
            var ordenes = await consulta.Select(c => new OrderDetailDto
            {
                Product = c.Product.Name,
                Quantity = c.Quantity,
                Price = c.Product.Price,
                Total = c.Product.Price * c.Quantity,
            }).SingleOrDefaultAsync();
            return ordenes;
        }

        public async Task UpdateAsync(Guid Id, CreateOrderDetailDto orderDetailDto)
        {
            var orderDetail = await repository.GetAsync(Id);
            if (orderDetail == null)
            {
                throw new NotFoundException($"La orden -> {Id} no puede ser actualizado");
            }
            orderDetail.OrderId = orderDetailDto.OrderId;
            orderDetail.ProductId = orderDetailDto.ProductId;
            orderDetail.Quantity = orderDetailDto.Quantity;
            orderDetail.Price = orderDetailDto.Price;
            orderDetail.Total = orderDetailDto.Quantity*orderDetailDto.Price;

            orderDetail.ModifiedDate = DateTime.Now;
            await repository.UpdateAsync(orderDetail);
        }
    }
}
