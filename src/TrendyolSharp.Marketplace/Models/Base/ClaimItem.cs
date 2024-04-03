namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class ClaimItem
{
  public OrderLine OrderLine { get; set; }
  public List<ClaimItemDetail> ClaimItems { get; set; }
}