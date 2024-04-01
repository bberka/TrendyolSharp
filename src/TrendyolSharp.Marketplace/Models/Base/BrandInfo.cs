namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class BrandInfo
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }
}