using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestUpdatePackageStatus
{
  public List<PackageStatusLine> Lines { get; set; }
  public PackageStatusParams StatusParams { get; set; }
  public string Status { get; set; }
}