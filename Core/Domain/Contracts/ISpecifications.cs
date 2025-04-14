
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecifications<T> where T : class
    {
        Expression<Func<T,bool>> Criteria { get; }
        List<Expression<Func<T, object>>> IncludeExpressions { get; }
    }
}
// where => Expression<Func<T,bool>> Criteria
//include=> List<expression<func<T,object>>>
//select => criteria expression<func<T,Tresult>>
