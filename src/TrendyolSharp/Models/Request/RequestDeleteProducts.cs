using System.Collections.Generic;
using TrendyolSharp.Models.Base;

namespace TrendyolSharp.Models.Request
{
  public sealed class RequestDeleteProducts
  {
    public List<ProductBarcode> Items { get; set; }
  }
}