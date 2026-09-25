namespace HomeLibrary.AL.DTOs;

/// <summary>
/// Списочный ответ с данными постраничного разбиения.
/// </summary>
/// <typeparam name="T">Тип элементов списка.</typeparam>
public class PagedListResponseDto<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int TotalPages => PerPage <= 0
        ? 0
        : (TotalCount / PerPage) + (TotalCount % PerPage > 0 ? 1 : 0);
}