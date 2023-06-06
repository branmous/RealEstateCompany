namespace RealEstate.Domain.Specifications
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable,
            int page, int recordsNumber)
        {
            return queryable
                .Skip((page - 1) * recordsNumber)
                .Take(recordsNumber);
        }
    }

}
