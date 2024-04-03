namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class Line
{
  public int Quantity { get; set; }
  public int SalesCampaignId { get; set; }
  public string ProductSize { get; set; }
  public string MerchantSku { get; set; }
  public string ProductName { get; set; }
  public int ProductCode { get; set; }
  public string ProductOrigin { get; set; }
  public int MerchantId { get; set; }
  public decimal Amount { get; set; }
  public decimal Discount { get; set; }
  public decimal TyDiscount { get; set; }
  public List<DiscountDetail> DiscountDetails { get; set; }
  public List<FastDeliveryOption> FastDeliveryOptions { get; set; }
  public string CurrencyCode { get; set; }
  public string ProductColor { get; set; }
  public int Id { get; set; }
  public string Sku { get; set; }
  public decimal VatBaseAmount { get; set; }
  public string Barcode { get; set; }
  public string OrderLineItemStatusName { get; set; }
  public decimal Price { get; set; }
}
