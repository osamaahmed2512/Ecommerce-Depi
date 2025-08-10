using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.domain.constants;
using Ecommerce.domain.Specification;
using Ecommerce.infrastructure.SpecificationEvaluator;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.infrastructure.percistence.Repository
{
    public class Repository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplcationDbContext _context;
        private DbSet<T> _entity;
        public Repository(ApplcationDbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }
        public async Task Add(T entity) => await _entity.AddAsync(entity);


        public async Task<T> Get(int id) => await _entity.FindAsync(id);


        public async Task<IEnumerable<T>> GetAll() => await _entity.ToListAsync();

        public void Update(T entity)
        {
            _entity.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            T data = await _entity.FindAsync(id);
            _entity.Remove(data);
        }

        public void DeleteRange(IEnumerable<T> Etities)
        {
            _entity.RemoveRange(Etities);
        }

        public async Task<IEnumerable<T>> GetPaged(int pageNumber, int pageSize)
        {
            return await _entity.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPaged(Expression<Func<T, bool>> criteria, int pageNumber, int pageSize,
           Expression<Func<T, object>> orderBy = null, string orderByDirection = Arrange.Descinding)
        {
            IQueryable<T> query = _entity.Where(criteria);

            if (orderBy != null)
            {
                query = orderByDirection == Arrange.Ascending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return await query.ToListAsync();
        }
        public async Task<int> GetTotalCount()

        {
            return await _entity.CountAsync();
        }

        public async Task<T> GetWhereFirst(Expression<Func<T, bool>> predicate)
        {
            return await _entity.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> predicate)
        {
            return await _entity.SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification = null)
        {
            return  ApplySpecificationForList (specification);
        }

        public async Task<T> FindAsync(ISpecification<T> specification = null)
        {
            return await ApplySpecificationForList(specification).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecificationForList(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_entity.AsQueryable(),spec);
        }
    }
}
