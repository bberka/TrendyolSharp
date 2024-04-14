using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace.Request
{
  public sealed class RequestSplitShipmentPackageByQuantity
  {
    public List<QuantitySplit> QuantitySplit { get; set; }
  }
}