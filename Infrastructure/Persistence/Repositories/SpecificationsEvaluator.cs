
using Domain.Contracts;

namespace Persistence.Repositories
{
    internal static class SpecificationsEvaluator
    {
        public static IQueryable<T> CreateQuery<T>(IQueryable<T> inputQuery,ISpecifications<T> specifications)
            where T:class
        {
            var query = inputQuery;
            if(specifications.Criteria is not null)
            {
                query = query.Where(specifications.Criteria);
            }
            //foreach (var include in specifications.IncludeExpressions)
            //  query =  query.Include(include);
            query = specifications.IncludeExpressions.Aggregate(query,
                (currentQuery, include)
                => currentQuery.Include(include));
            return query;
        }
    }
}

//set<T>,Specifications<T>
