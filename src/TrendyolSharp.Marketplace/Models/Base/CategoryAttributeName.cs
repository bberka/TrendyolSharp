namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class CategoryAttributeName
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }
}