namespace TrendyolSharp.Marketplace.Models.GetCategoryAttributes;


public class GetCategoryAttributesResponse
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }

  [Required]
  public string DisplayName { get; set; }

  public List<GetCategoryAttributes_CategoryAttribute> CategoryAttributes { get; set; }
}