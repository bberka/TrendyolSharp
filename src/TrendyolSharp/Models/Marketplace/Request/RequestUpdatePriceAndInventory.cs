using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace.Request
{
  public sealed class RequestUpdatePriceAndInventory
  {
    public List<ProductPriceAndInventoryInfo> Items { get; set; }
  }
}