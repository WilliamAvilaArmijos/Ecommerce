using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Curso.ComercioElectronico.Dominio.Insercion
{
    public static class DominioServiceCollectionExtensions
    {
        public static IServiceCollection AddDominio(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }
    }
}
