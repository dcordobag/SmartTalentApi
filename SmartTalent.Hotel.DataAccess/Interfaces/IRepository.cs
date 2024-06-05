using SmartTalent.Hotel.DataAccess.Database.Interfaces;
namespace SmartTalent.Hotel.DataAccess.Interfaces
{
    using System.Linq.Expressions;

    public interface IRepository<T> where T : class, new()
    {
        IDatabaseContext RepositoryContext { get; }

        long Count(Expression<Func<T, bool>> expression);

        T Search(Expression<Func<T, bool>> expression);
        Task<T> SearchAsync(Expression<Func<T, bool>> expression);
        Task<T> SearchLastAsync(Expression<Func<T, bool>> expression);
        Task<T> SearchAsync();
        Task<long> CountAsync(Expression<Func<T, bool>> expression);

        Task<ICollection<T>> SearchListAsync(Expression<Func<T, bool>> expression);

        Task<T> CreateAsync(T objCreate);

        Task<bool?> EditAsync(T objEdit);

        Task<ICollection<T>> SearchListAsync();

        Task<bool?> DeleteAsync(Expression<Func<T, bool>> expression);
        Task<bool?> DeleteAsync(T objDelete);

    }
}
