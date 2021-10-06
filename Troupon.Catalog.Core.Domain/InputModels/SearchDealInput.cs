using System.Collections;
using System.Collections.Generic;
using Troupon.Catalog.Core.Domain.Entities.Common;

namespace Troupon.Catalog.Core.Domain.InputModels
{
  public class SearchDealsFilter : PageableParameter
  {
    public string SearchText { get; }
    public string Location { get; }
    public IEnumerable<string> Categories { get; }
    public IEnumerable<MinMax> PriceRanges { get; }
    public IEnumerable<MinMax> DistanceRanges { get; }

    public SearchDealsFilter(string searchText,
      string location,
      IEnumerable<string> categories,
      IEnumerable<MinMax> priceRanges,
      IEnumerable<MinMax> distanceRanges,
      PagingInfo? pagingInfo) : base(pagingInfo)
    {
      SearchText = searchText;
      Location = location;
      Categories = categories;
      PriceRanges = priceRanges;
      DistanceRanges = distanceRanges;
    }
  }

  public abstract class PageableParameter : IPageable
  {
    public PagingInfo PagingInfo { get; }

    public PageableParameter(PagingInfo? info)
    {
      PagingInfo = info ?? new PagingInfo();
    }
  }

  public interface IPageable
  {
    PagingInfo PagingInfo { get; }
  }

  public class PagingInfo
  {
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 50;
  }
}
