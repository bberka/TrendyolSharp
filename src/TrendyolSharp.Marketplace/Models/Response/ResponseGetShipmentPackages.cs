using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Response;

public sealed class ResponseGetShipmentPackages
{
  public long Page { get; set; }
  public long Size { get; set; }
  public long TotalPages { get; set; }
  public long TotalElements { get; set; }
  public List<ShipmentPackage> Content { get; set; }
  public ShipmentAddress ShipmentAddress { get; set; }
  public string OrderNumber { get; set; }
  public decimal GrossAmount { get; set; }
  public decimal TotalDiscount { get; set; }
  public decimal TotalTyDiscount { get; set; }
  public InvoiceAddress InvoiceAddress { get; set; }
  public string CustomerFirstName { get; set; }
  public string CustomerEmail { get; set; }
  public long CustomerId { get; set; }
  public string CustomerLastName { get; set; }
  public long Id { get; set; }
  public long CargoTrackingNumber { get; set; }
  public string CargoTrackingLink { get; set; }
  public string CargoSenderNumber { get; set; }
  public string CargoProviderName { get; set; }
  public List<Line> Lines { get; set; }
  public long OrderDate { get; set; }
  public string TcIdentityNumber { get; set; }
  public string CurrencyCode { get; set; }
  public List<PackageHistory> PackageHistories { get; set; }
  public string ShipmentPackageStatus { get; set; }
  public string Status { get; set; }
  public string DeliveryType { get; set; }
  public long TimeSlotId { get; set; }
}