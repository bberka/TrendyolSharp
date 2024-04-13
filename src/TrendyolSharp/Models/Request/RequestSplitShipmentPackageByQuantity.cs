using System.Collections.Generic;
using TrendyolSharp.Models.Base;

namespace TrendyolSharp.Models.Request
{
  public sealed class RequestSplitShipmentPackageByQuantity
  {
    public List<QuantitySplit> QuantitySplit { get; set; }
  }
}