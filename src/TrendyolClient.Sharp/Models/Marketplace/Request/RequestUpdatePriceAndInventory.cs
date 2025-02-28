using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request
{
  public sealed class RequestUpdatePriceAndInventory
  {
    public List<ProductPriceAndInventoryInfo> Items { get; set; }
  }
}