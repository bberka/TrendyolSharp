namespace TrendyolSharp.Marketplace.Models.GetCategoryAttributes;

public class GetCategoryAttributes_AttributeValue
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }
}