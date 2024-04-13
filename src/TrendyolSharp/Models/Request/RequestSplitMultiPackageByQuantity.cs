using System.Collections.Generic;
using TrendyolSharp.Models.Base;

namespace TrendyolSharp.Models.Request
{
  public sealed class RequestSplitMultiPackageByQuantity
  {
    public List<PackageDetailGroup> SplitPackages { get; set; }
  }
}