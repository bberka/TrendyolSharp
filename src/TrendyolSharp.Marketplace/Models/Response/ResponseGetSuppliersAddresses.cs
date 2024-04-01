using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Response;

public sealed class ResponseGetSuppliersAddresses
{
  public SupplierAddress[] SupplierAddresses { get; set; }

  public SupplierAddress DefaultShipmentAddress { get; set; }

  public SupplierAddress DefaultInvoiceAddress { get; set; }

  public DefaultReturningAddress GetSuppliersAddressesDefaultReturningAddress { get; set; }
}