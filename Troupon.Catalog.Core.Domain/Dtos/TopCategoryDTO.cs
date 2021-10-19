using System.Collections.Generic;
using System.Linq;

namespace Troupon.Catalog.Core.Domain.Dtos
{
  public class TopCategoryDTO
  {
    public string Description { get; set; } = "";
    public IEnumerable<CategoryDto> Categories { get; set; } = Enumerable.Empty<CategoryDto>();
  }

  public class CategoryDto
  {
    public string Description { get; set; } = "";
    public int DealsAmount { get; set; }

    public IEnumerable<SubCategoryDto> SubCategories { get; set; } = Enumerable.Empty<SubCategoryDto>();
  }

  public class SubCategoryDto
  {
    public string Description { get; set; } = "";
    public int DealsAmount { get; set; }    
  }
}
