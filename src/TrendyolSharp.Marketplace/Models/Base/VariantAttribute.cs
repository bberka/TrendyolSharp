namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class VariantAttribute
{
  [Required]
  public string AttributeName { get; set; }

  [Required]
  public string AttributeValue { get; set; }
}