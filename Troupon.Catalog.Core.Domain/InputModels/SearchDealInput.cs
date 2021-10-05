using System;
using System.ComponentModel.DataAnnotations;
using Troupon.Catalog.Core.Domain.Entities.Common;
using Troupon.Catalog.Core.Domain.Enums;

namespace Troupon.Catalog.Core.Domain.InputModels
{
  public record SearchDealsFilter(
      string searchText,
      string location,
      string[] categories,
      MinMax[] priceRanges,
      MinMax[] distanceRanges
    );
}
