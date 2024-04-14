namespace TrendyolSharp.Models.Marketplace.Request
{
  public sealed class RequestGetClaims
  {
    public string ClaimIds { get; set; }
    public string ClaimItemStatus { get; set; }
    public long? EndDate { get; set; }
    public long? StartDate { get; set; }
    public string OrderNumber { get; set; }
    public int? Size { get; set; }
    public int? Page { get; set; }
  }
}