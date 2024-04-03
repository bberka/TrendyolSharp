using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestCreateClaim
{
  public List<CreateClaimItem> ClaimItems { get; set; }
  public int CustomerId { get; set; }
  public bool ExcludeListing { get; set; }
  public bool ForcePackageCreation { get; set; }
  public string OrderNumber { get; set; }
  public int ShipmentCompanyId { get; set; }
}