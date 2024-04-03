namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestProcessAlternativeDelivery
{
  public bool IsPhoneNumber { get; set; }
  public string TrackingInfo { get; set; }
  public Dictionary<string, object> Params { get; set; }
  public int? BoxQuantity { get; set; }
  public decimal? Deci { get; set; }
}