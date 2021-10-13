using System.Collections;
using System.Collections.Generic;
using Troupon.Catalog.Core.Domain.Entities.Common;

namespace Troupon.Catalog.Core.Domain.InputModels
{
  public class SearchDealsFilter
  {
    public string SearchText { get; }
    public string Location { get; }
    public IEnumerable<string> Categories { get; }
    public IEnumerable<MinMax> PriceRanges { get; }
    public IEnumerable<MinMax> DistanceRanges { get; }

    public SearchDealsFilter(
      string searchText,
      string location,
      IEnumerable<string> categories,
      IEnumerable<MinMax> priceRanges,
      IEnumerable<MinMax> distanceRanges)
    {
      SearchText = searchText;
      Location = location;
      Categories = categories;
      PriceRanges = priceRanges;
      DistanceRanges = distanceRanges;
    }
  }
}
