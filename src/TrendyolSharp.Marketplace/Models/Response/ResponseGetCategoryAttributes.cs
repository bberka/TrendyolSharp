using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Response;

public class ResponseGetCategoryAttributes
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }

  [Required]
  public string DisplayName { get; set; }

  public List<CategoryAttribute> CategoryAttributes { get; set; }
}