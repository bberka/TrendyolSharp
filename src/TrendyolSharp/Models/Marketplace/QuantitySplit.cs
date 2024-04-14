using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace
{
  public sealed class QuantitySplit
  {
    public long OrderLineId { get; set; }
    public List<long> Quantities { get; set; }
  }
}