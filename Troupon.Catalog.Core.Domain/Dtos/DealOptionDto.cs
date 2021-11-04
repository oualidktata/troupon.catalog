using System;

namespace Troupon.Catalog.Core.Domain.Dtos
{
  public record DealOptionDto
  (
    Guid Id,
    string Description,
    decimal OriginalPrice,
    decimal CurrentPrice,
    int SoldAmount
  );  
}
