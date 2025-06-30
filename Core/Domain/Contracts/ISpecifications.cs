
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecifications<T> where T : class
    {
        Expression<Func<T,bool>> Criteria { get; }
        List<Expression<Func<T, object>>> IncludeExpressions { get; }
        Expression<Func<T,object>> OrderBy { get; }
        Expression<Func<T,object>> OrderByDescending { get; }
        int Skip {  get; }
        int Take {  get; }
        bool IsPaginated {  get; }
    }
}
// where => Expression<Func<T,bool>> Criteria
//include=> List<expression<func<T,object>>>
//select => criteria expression<func<T,Tresult>>
