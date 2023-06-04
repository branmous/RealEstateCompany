namespace RealEstate.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> All();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
