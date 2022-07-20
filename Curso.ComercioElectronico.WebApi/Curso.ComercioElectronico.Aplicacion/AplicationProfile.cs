using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;

namespace Curso.ComercioElectronico.Aplicacion
{
    public class AplicationProfile : Profile
    {
        public AplicationProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<ProductTypeDto, ProductType>();
            //origen - destino
            CreateMap<CreateProductDto, Product>();
            CreateMap<CreateProductTypeDto, ProductType>();
            CreateMap<CreateBrandDto, Brand>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<CreateOrderDetailDto, OrderDetail>();
            CreateMap<CreateOrderDto, Order>();
        }
    }
}
