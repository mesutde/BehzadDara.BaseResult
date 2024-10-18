namespace BehzadDara.BaseResult;

public class PagedResult<T>(int pageSize, int pageNumber, int totalCount, IReadOnlyList<T> data)
{
    public int PageSize { get; set; } = pageSize;
    public int PageNumber { get; set; } = pageNumber;
    public int TotalCount { get; set; } = totalCount;
    public IEnumerable<T> Data { get; set; } = data;

    public static PagedResult<T> Create(int pageSize, int pageNumber, int totalCount, IReadOnlyList<T> data)
    {
        return new PagedResult<T>(pageSize, pageNumber, totalCount, data);
    }
}