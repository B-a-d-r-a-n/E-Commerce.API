
using System.Linq.Expressions;

namespace Services.Specifications
{
    internal abstract class BaseSpecifications<T>
        : ISpecifications<T> where T : class
    {

        protected BaseSpecifications(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; private set; }

        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<T, object>> include)
            => IncludeExpressions.Add(include);
    }
}
