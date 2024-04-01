namespace TrendyolSharp.Marketplace.Models.GetCategoryAttributes;

public class GetCategoryAttributes_Attribute
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }
}