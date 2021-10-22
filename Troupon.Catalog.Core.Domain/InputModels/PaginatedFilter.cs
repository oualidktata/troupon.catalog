namespace Troupon.Catalog.Core.Domain.InputModels
{
  public record PaginatedFilter
  {
    public int Offset { get; init; }
    public int Limit { get; init; }    
  }
}
