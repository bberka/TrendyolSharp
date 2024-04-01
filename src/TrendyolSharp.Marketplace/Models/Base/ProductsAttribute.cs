namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class ProductsAttribute
{
  public int AttributeId { get; set; }
  public string AttributeName { get; set; }
  public int? AttributeValueId { get; set; }
  public string AttributeValue { get; set; }
}