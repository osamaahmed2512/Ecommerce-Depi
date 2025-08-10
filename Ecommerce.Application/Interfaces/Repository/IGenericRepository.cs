using Ecommerce.domain.constants;
using Ecommerce.domain.Specification;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Ecommerce.Application.Interfaces.Repository
{
    public interface IGenericRepository <T> where T : class
    {
        Task Add(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        void Update(T entity);
        Task Delete(int id);
        void DeleteRange(IEnumerable<T> Etities);
        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification = null);
        Task<T> FindAsync(ISpecification<T> specification = null);
        Task<IEnumerable<T>> GetPaged(int pageNumber, int pageSize);
        Task<IEnumerable<T>> GetPaged(Expression<Func<T, bool>> criteria, int pageNumber, int pageSize,
           Expression<Func<T, object>> orderBy = null, string orderByDirection = Arrange.Ascending);
        Task<int> GetTotalCount();
        Task<T> GetWhereFirst(Expression<Func<T, bool>> predicate);
        Task<T> GetSingle(Expression<Func<T, bool>> predicate);
    }
}
