namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class UpdateProduct
{
  [Required]
  [StringLength(40)]
  public string Barcode { get; set; }

  [Required]
  [StringLength(100)]
  public string Title { get; set; }

  [Required]
  [StringLength(40)]
  public string ProductMainId { get; set; }

  [Required]
  public int BrandId { get; set; }

  [Required]
  public int CategoryId { get; set; }

  [Required]
  [StringLength(100)]
  public string StockCode { get; set; }

  [Required]
  public decimal DimensionalWeight { get; set; }

  [Required]
  [StringLength(30000)]
  public string Description { get; set; }

  [Required]
  public int VatRate { get; set; }

  public int? DeliveryDuration { get; set; }

  public DeliveryOption DeliveryOption { get; set; }

  [Required]
  public List<Image> Images { get; set; }

  [Required]
  public List<Attribute> Attributes { get; set; }

  [Required]
  public int CargoCompanyId { get; set; }

  public int? ShipmentAddressId { get; set; }

  public int? ReturningAddressId { get; set; }
}