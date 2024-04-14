using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace.Request
{
  public sealed class RequestDeleteProducts
  {
    public List<ProductBarcode> Items { get; set; }
  }
}