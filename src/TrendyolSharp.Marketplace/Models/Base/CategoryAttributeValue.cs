namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class CategoryAttributeValue
{
  public long Id { get; set; }

  [Required]
  public string Name { get; set; }
}