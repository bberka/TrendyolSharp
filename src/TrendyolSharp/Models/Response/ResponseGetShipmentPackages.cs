using System.Collections.Generic;
using TrendyolSharp.Models.Base;

namespace TrendyolSharp.Models.Response
{
  public sealed class ResponseGetShipmentPackages
  {
    public long Page { get; set; }
    public long Size { get; set; }
    public long TotalPages { get; set; }
    public long TotalElements { get; set; }
    public List<ShipmentPackage> Content { get; set; }
  }
}