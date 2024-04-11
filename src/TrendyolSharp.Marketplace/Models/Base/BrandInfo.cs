namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class BrandInfo
{
  public long Id { get; set; }

  [Required]
  public string Name { get; set; }
}