using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestSplitShipmentPackageByQuantity
{
  public List<QuantitySplit> QuantitySplit { get; set; }
}