﻿using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace.Request
{
  public sealed class RequestUpdatePackageStatus
  {
    public List<PackageStatusLine> Lines { get; set; }
    public PackageStatusParams StatusParams { get; set; }
    public string Status { get; set; }
  }
}