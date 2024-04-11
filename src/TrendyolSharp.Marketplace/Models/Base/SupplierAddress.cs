namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class SupplierAddress
{
  public long Id { get; set; }

  [Required]
  public string AddressType { get; set; }

  [Required]
  public string Country { get; set; }

  [Required]
  public string City { get; set; }

  public long CityCode { get; set; }

  [Required]
  public string District { get; set; }

  public long DistrictId { get; set; }

  [Required]
  public string PostCode { get; set; }

  [Required]
  public string Address { get; set; }

  public bool ReturningAddress { get; set; }

  [Required]
  public string FullAddress { get; set; }

  public bool ShipmentAddress { get; set; }

  public bool InvoiceAddress { get; set; }

  public bool Default { get; set; }
}