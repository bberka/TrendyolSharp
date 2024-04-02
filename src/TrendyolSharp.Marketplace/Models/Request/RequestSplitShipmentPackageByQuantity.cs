using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Request;

public class RequestSplitShipmentPackageByQuantity
{
  public List<QuantitySplit> QuantitySplit { get; set; }
}