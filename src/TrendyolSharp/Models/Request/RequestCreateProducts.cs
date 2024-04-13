using System.Collections.Generic;
using TrendyolSharp.Models.Base;

namespace TrendyolSharp.Models.Request
{
  public sealed class RequestCreateProducts
  {
    public List<CreateProduct> Items { get; set; }
  }
}