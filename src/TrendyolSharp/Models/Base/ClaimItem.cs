using System.Collections.Generic;

namespace TrendyolSharp.Models.Base
{
  public sealed class ClaimItem
  {
    public OrderLine OrderLine { get; set; }
    public List<ClaimItemDetail> ClaimItems { get; set; }
  }
}