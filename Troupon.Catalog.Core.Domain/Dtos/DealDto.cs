using System;
using Troupon.Catalog.Core.Domain.Entities.Common;

namespace Troupon.Catalog.Core.Domain.Dtos
{
  public class DealDto
  {
    public Guid Id { get; init; }
    public MultimediaDto? Image { get; init; }

    public string Title { get; init; } = "";
    public string Address { get; init; } = "";
    public decimal OriginalPrice { get; init; }
    public decimal CurrentPrice { get; init; }
    public float AverageRating { get; init; }
    public int RatingQuantity { get; init; }
    public float Distance { get; init; }
    public string Subtitle { get; init; } = "";

    public Position? Position { get; init; }
    public bool IsTrending { get; init; }
    public int Views { get; init; }
  }
}
