using System;
using System.Collections.Generic;
using System.Linq;
using Troupon.Catalog.Core.Domain.Entities.Common;
using Troupon.Catalog.Core.Domain.Enums;

namespace Troupon.Catalog.Core.Domain.Dtos
{
  public record DealDetailsDto
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string Highlights { get; set; } = "";
    public string Description { get; set; } = "";
    public IEnumerable<MultimediaDto> Images { get; set; } = Enumerable.Empty<MultimediaDto>();
    public DateTime ExpirationDate { get; set; }
    public DealStatus Status { get; set; }
    public Guid AccountId { get; set; }
    public string MerchantName { get; set; } = "";
    public string Address { get; set; } = "";
    public IEnumerable<CustomerReviewDto> CustomerReviews { get; set; } = Enumerable.Empty<CustomerReviewDto>();
    public Position? Position { get; set; }
    public string? Website { get; set; }
    public IEnumerable<DealOptionDto> DealOptions { get; set; } = Enumerable.Empty<DealOptionDto>();
    public int Views { get; set; }
  }

  public record CustomerReviewDto
  (
      int Rating,
      string? Comment
  );
}
