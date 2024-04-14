using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace.Request
{
  public sealed class RequestSplitMultiPackageByQuantity
  {
    public List<PackageDetailGroup> SplitPackages { get; set; }
  }
}