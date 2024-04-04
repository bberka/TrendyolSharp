namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class ClaimAuditInformation
{
  public string ClaimId { get; set; }
  public string ClaimItemId { get; set; }
  public string PreviousStatus { get; set; }
  public string NewStatus { get; set; }
  public ExecutorUserInfoDocument UserInfoDocument { get; set; }
  public long Date { get; set; }
}