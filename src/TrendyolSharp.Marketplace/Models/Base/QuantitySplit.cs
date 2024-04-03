namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class QuantitySplit
{
  public long OrderLineId { get; set; }
  public List<int> Quantities { get; set; }
}