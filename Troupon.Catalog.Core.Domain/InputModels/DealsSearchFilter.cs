using System.Collections.Generic;
using Troupon.Catalog.Core.Domain.Entities.Common;

namespace Troupon.Catalog.Core.Domain.InputModels
{
  public class DealsSearchFilter : PaginatedFilter
  {
      public string Location { get; init; } = "";
      public string? Query { get; init; }
      public IEnumerable<string>? Categories { get; init; }
      public IEnumerable<string>? Cities { get; init; }
      public IEnumerable<MinMax>? PriceRanges { get; init; }
      public IEnumerable<MinMax>? DistanceRanges { get; init; }
      public float? Rating { get; init; }
      public bool? WithMapView { get; init; }
      public DealsSortByEnum? SortBy { get; init; }
  }


  public enum DealsSortByEnum
  {
    Relevance,
    PriceAscending,
    PriceDescending,
    Distance,
    Rating
  }
}
