using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace.Request
{
  public sealed class RequestMultiSplitShipmentPackage
  {
    public List<SplitGroup> SplitGroups { get; set; }
  }
}