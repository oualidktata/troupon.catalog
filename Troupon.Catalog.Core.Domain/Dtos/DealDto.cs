using System;
using Troupon.Catalog.Core.Domain.Entities.Common;

namespace Troupon.Catalog.Core.Domain.Dtos
{
  public class DealDto
  {
    public Guid Id { get; set; }
    public MultimediaDto? Image { get; set; }

    public string Title { get; set; } = "";
    public string Address { get; set; } = "";
    public decimal OriginalPrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public float AverageRating { get; set; }
    public int RatingQuantity { get; set; }
    public float Distance { get; set; }
    public string Subtitle { get; set; } = "";

    public Position? Position { get; set; }
    public bool IsTrending { get; set; }
    public int Views { get; set; }
  }
}
