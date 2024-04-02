using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestSplitMultiPackageByQuantity
{
  public List<PackageDetailGroup> SplitPackages { get; set; }
}