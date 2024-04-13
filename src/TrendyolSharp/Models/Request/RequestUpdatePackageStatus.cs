using System.Collections.Generic;
using TrendyolSharp.Models.Base;

namespace TrendyolSharp.Models.Request
{
  public sealed class RequestUpdatePackageStatus
  {
    public List<PackageStatusLine> Lines { get; set; }
    public PackageStatusParams StatusParams { get; set; }
    public string Status { get; set; }
  }
}