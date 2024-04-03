namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class CreateClaimItem
{
  public string Barcode { get; set; }
  public string CustomerNote { get; set; }
  public int Quantity { get; set; }
  public int ReasonId { get; set; }
}