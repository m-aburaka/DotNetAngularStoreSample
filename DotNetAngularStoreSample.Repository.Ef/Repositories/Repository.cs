using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetAngularStoreSample.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DotNetAngularStoreSample.Repository.Ef.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext Context;

        public Repository(AppDbContext context)
        {
            Context = context;
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await Context.FindAsync<T>(id);
            return entity != null;
        }

        public async Task<int> Count()
        {
            return await Context.Set<T>().CountAsync();
        }

        public async Task<IList<T>> Get()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> GetPage(int page, int pageSize)
        {
            var skip = page * pageSize;
            var list = await Context.Set<T>()
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            return list;
        }

        public async Task Insert(T entity)
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await Get(id);
            if (entity != null)
                await Delete(entity);

        }
    }
}