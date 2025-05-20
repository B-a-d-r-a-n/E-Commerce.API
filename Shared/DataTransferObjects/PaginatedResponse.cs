

namespace Shared.DataTransferObjects
{
    public record PaginatedResponse<TData>
        (int PageIndex,int PageSize,int TotalCount,IEnumerable<TData> data)
    {

    }
}
