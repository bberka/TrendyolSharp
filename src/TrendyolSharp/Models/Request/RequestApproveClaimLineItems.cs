using System.Collections.Generic;

namespace TrendyolSharp.Models.Request
{
  public sealed class RequestApproveClaimLineItems
  {
    public List<string> ClaimLineItemIdList { get; set; }
    public Dictionary<string, object> Params { get; set; }
  }
}