namespace netcore_api_template.Models;
public class QueryParameters
{
    private const int MaxPageSize = 100;
    private int pageSize = 10;
    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get => pageSize;
        set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public string? Sort { get; set; } = "-CreatedAt";
    public string? Filter { get; set; }
}
