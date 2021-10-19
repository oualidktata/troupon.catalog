using System.Collections;
using System.Collections.Generic;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities.Common;

namespace Troupon.Catalog.Core.Domain.InputModels
{   
  public record DealsSearchFilter
  (
    string Location,
    string? Query,    
    IEnumerable<string>? Categories,
    IEnumerable<string>? Cities,
    IEnumerable<MinMax>? PriceRanges,
    IEnumerable<MinMax>? DistanceRanges,
    float? Rating,
    bool? WithMapView,
    DealsSortByEnum? SortBy,
    IEnumerable<DealOptionDto>? DealOptions
  );

  public enum DealsSortByEnum
  {
    Relevance,
    PriceAscending,
    PriceDescending,
    Distance,
    Rating
  }
}
