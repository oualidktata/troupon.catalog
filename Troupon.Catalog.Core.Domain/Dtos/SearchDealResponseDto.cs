using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troupon.Catalog.Core.Domain.Entities.Common;

namespace Troupon.Catalog.Core.Domain.Dtos
{
  public class SearchDealResponseDto
  {
    public Guid Id { get; set; }
    public double Price { get; set; }
    public bool HasOptions { get; set; }
    public ImageDescription Image { get; set; }
    public int QuantityBought { get; set; }
  }
}
