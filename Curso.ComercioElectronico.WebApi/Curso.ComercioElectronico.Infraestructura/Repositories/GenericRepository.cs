using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Dominio.Entities.Base;
using Curso.ComercioElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructura.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ComercioElectronicoDbContext context;

        public GenericRepository(ComercioElectronicoDbContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<T>> GetAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(string code)
        {
            return await context.Set<T>().FindAsync(code);
        }

        public async Task<T> GetAsync(Guid Id)
        {
            return await context.Set<T>().FindAsync(Id);
        }

        public async Task CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
           
        }

        public async Task UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return context.Set<T>().AsQueryable();
        }

        public async Task<ICollection<T>> GetListAsync(int limit = 10, int offset = 0)
        {
            return await context.Set<T>()
                .Take(limit)
                .Skip(offset)
                .ToListAsync();
        }
    }
}
