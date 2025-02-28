using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request
{
  public sealed class RequestDeleteProducts
  {
    public List<ProductBarcode> Items { get; set; }
  }
}