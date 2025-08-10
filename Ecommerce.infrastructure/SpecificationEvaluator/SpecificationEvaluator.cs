using Ecommerce.domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.infrastructure.SpecificationEvaluator
{
    public static class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }else if(spec.OrderByDescending !=null) {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            if(spec.GroupBy != null)
            {
                query = query.GroupBy(spec.GroupBy).SelectMany(x => x);
            }
            if (spec.IspagingEnabled)
            {
                query = query.Skip(spec.Skip.Value).Take(spec.Take.Value);
            }
            return query;
        }
    
    }
}
