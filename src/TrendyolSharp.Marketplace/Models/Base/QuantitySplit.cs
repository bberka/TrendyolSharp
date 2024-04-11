namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class QuantitySplit
{
  public long OrderLineId { get; set; }
  public List<long> Quantities { get; set; }
}