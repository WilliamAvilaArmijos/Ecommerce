using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Aplicacion.ServicesImpl;
using FluentValidation; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Curso.ComercioElectronico.Aplicacion.Insercion
{
    public static class AplicacionServiceCollectionExtensions
    {
        public static IServiceCollection AddAplicacion(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IProductAppService, ProductAppService>();
            services.AddTransient<IProductTypeAppService, ProductTypeAppService>();
            services.AddTransient<IBrandAppService, BrandAppService>();
            services.AddTransient<ICustomerAppService, CustomerAppService>();
            services.AddTransient<IOrderDetailAppService, OrderDetailAppService>();
            services.AddTransient<IOrderAppService, OrderAppService>();

            //Validaciones
            //services.AddValidatorsFromAssemblyContaining<CreateBrandDtoValidator>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
