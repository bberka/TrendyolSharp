namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestClaimIssue
{
  public string ClaimId { get; set; }
  public long ClaimIssueReasonId { get; set; }
  public string ClaimItemIdList { get; set; }
  public string Description { get; set; }
}