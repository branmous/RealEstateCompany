namespace RealEstate.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> All();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
