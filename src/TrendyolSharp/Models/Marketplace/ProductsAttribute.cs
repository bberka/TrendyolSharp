﻿namespace TrendyolSharp.Models.Marketplace
{
  public sealed class ProductsAttribute
  {
    public long AttributeId { get; set; }
    public string AttributeName { get; set; }
    public long? AttributeValueId { get; set; }
    public string AttributeValue { get; set; }
  }
}