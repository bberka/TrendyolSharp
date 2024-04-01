namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class Attribute
{
  public int AttributeId { get; set; }

  public int AttributeValueId { get; set; }

  public string? CustomAttributeValue { get; set; }
}