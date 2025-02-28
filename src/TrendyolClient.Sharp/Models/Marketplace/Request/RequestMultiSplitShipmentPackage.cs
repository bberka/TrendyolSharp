using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request
{
  public sealed class RequestMultiSplitShipmentPackage
  {
    public List<SplitGroup> SplitGroups { get; set; }
  }
}