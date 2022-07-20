using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Dominio.Entities.Base;

namespace Curso.ComercioElectronico.Dominio.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        //LISTAR TODOS LOS OBJETOS DE UNA ENTIDAD QUE SE DECLARA AL INICIALIZAR EL REPOSITORIO
        Task<ICollection<T>> GetAsync();
        //OBTENER LOS OBJETOS POR SU CLAVE PRIMARIA
        Task<ICollection<T>> GetListAsync(int limit = 10, int offset=0);
        Task<T> GetAsync(string code);

        Task<T> GetAsync(Guid Id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
        IQueryable<T> GetQueryable();
    }
}
