namespace TrendyolSharp.Marketplace.Models.Request;

public class RequestSendInvoiceLink
{
  public string InvoiceLink { get; set; }
  public long ShipmentPackageId { get; set; }
  public long InvoiceDateTime { get; set; }
  public string InvoiceNumber { get; set; }
}