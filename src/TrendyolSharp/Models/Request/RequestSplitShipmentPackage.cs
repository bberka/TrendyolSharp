using System.Collections.Generic;

namespace TrendyolSharp.Models.Request
{
  public sealed class RequestSplitShipmentPackage
  {
    public List<long> OrderLineIds { get; set; }
  }
}