using System.Collections.Generic;

namespace TrendyolSharp.Models.Base
{
  public sealed class QuantitySplit
  {
    public long OrderLineId { get; set; }
    public List<long> Quantities { get; set; }
  }
}