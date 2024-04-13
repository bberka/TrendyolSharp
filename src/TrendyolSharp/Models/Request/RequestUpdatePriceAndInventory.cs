using System.Collections.Generic;
using TrendyolSharp.Models.Base;

namespace TrendyolSharp.Models.Request
{
  public sealed class RequestUpdatePriceAndInventory
  {
    public List<ProductPriceAndInventoryInfo> Items { get; set; }
  }
}