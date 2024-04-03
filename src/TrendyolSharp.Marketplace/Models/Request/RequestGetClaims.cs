namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestGetClaims
{
  public string? ClaimIds { get; set; }
  public string? ClaimItemStatus { get; set; }
  public int? EndDate { get; set; }
  public int? StartDate { get; set; }
  public string? OrderNumber { get; set; }
  public int? Size { get; set; }
  public int? Page { get; set; }
}