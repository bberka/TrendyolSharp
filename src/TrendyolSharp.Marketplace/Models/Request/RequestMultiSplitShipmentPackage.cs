using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestMultiSplitShipmentPackage
{
  public List<SplitGroup> SplitGroups { get; set; }
}