using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly DataContext Context;

        public BaseRepository(DataContext dbContext)
        {
            Context = dbContext;
        }
        public async Task AddAsync(T entity)
        {
            Context.Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<List<T>> All()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }
    }
}
