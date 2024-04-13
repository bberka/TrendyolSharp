using System.Collections.Generic;
using TrendyolSharp.Models.Base;

namespace TrendyolSharp.Models.Request
{
  public sealed class RequestMultiSplitShipmentPackage
  {
    public List<SplitGroup> SplitGroups { get; set; }
  }
}