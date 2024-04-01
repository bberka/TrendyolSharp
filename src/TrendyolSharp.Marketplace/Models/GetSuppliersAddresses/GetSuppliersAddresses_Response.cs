namespace TrendyolSharp.Marketplace.Models.GetSuppliersAddresses;

public class GetSuppliersAddresses_Response
{
  public GetSuppliersAddresses_SupplierAddress[] SupplierAddresses { get; set; }

  public GetSuppliersAddresses_SupplierAddress DefaultShipmentAddress { get; set; }

  public GetSuppliersAddresses_SupplierAddress DefaultInvoiceAddress { get; set; }

  public GetSuppliersAddresses_DefaultReturningAddress GetSuppliersAddressesDefaultReturningAddress { get; set; }
}