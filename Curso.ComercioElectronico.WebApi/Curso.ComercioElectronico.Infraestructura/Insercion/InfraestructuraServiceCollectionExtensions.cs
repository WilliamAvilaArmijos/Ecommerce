using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Dominio.Repositories;
using Curso.ComercioElectronico.Infraestructura.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Curso.ComercioElectronico.Infraestructura.Insercion
{
    public static class InfraestructuraServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraestructura(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ComercioElectronicoDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("ComercioElectronico"));
            });
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            return services;
        }
    }
}
