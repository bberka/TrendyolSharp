namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestApproveClaimLineItems
{
  public List<string> ClaimLineItemIdList { get; set; }
  public Dictionary<string, object> Params { get; set; }
}