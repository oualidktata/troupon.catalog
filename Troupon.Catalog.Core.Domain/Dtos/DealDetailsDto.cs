using System;
using System.Collections.Generic;
using System.Linq;
using Troupon.Catalog.Core.Domain.Entities.Common;
using Troupon.Catalog.Core.Domain.Enums;

namespace Troupon.Catalog.Core.Domain.Dtos
{
  public class DealDetailsDto
  {
    public Guid Id { get; init; }
    public string Title { get; init; } = "";
    public string Subtitle { get; init; } = "";
    public string Highlights { get; init; } = "";
    public string Description { get; init; } = "";
    public IEnumerable<MultimediaDto> Images { get; init; } = Enumerable.Empty<MultimediaDto>();
    public DateTime ExpirationDate { get; init; }
    public DealStatus Status { get; init; }
    public Guid AccountId { get; init; }
    public string MerchantName { get; init; } = "";
    public string Address { get; init; } = "";
    public IEnumerable<CustomerReviewDto> CustomerReviews { get; init; } = Enumerable.Empty<CustomerReviewDto>();
    public Position? Position { get; init; }
    public string? Website { get; init; }
    public IEnumerable<DealOptionDto> DealOptions { get; init; } = Enumerable.Empty<DealOptionDto>();
    public int Views { get; init; }
  }

  public record CustomerReviewDto
  (
      int Rating,
      string? Comment
  );
}
