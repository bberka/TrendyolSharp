using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace.Request
{
  public sealed class RequestSplitShipmentPackage
  {
    public List<long> OrderLineIds { get; set; }
  }
}