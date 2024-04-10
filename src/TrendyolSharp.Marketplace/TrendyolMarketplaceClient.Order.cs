using System.Runtime;
using System.Text;
using Serilog;
using TrendyolSharp.Marketplace.Models;
using TrendyolSharp.Marketplace.Models.Filter;
using TrendyolSharp.Marketplace.Models.Request;
using TrendyolSharp.Marketplace.Models.Response;
using TrendyolSharp.Shared;
using TrendyolSharp.Shared.Common;
using TrendyolSharp.Shared.Extensions;
using TrendyolSharp.Shared.Models;
using TrendyolSharp.Shared.Utils;


namespace TrendyolSharp.Marketplace;

public partial class TrendyolMarketplaceClient
{

  #region ORDER INTEGRATION

  //LINK: https://developers.trendyol.com/docs/category/sipari%C5%9F-entegrasyonu
  //CREATE TEST PRODUCT: https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/test-siparisi-olusturma

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-cekme
  ///  <br/><br/>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/askidaki-siparis-paketlerini-cekme
  ///  <br/><br/>
  /// You can retrieve information about every order placed by customers, which corresponds to the products you have submitted to the Trendyol system and is in payment control, using this method. After the payment control by the system, the orders are automatically packaged, and order packages are created.
  ///  <br/><br/>
  /// If you do not use any parameter in the endpoint below, it returns you maximum one week order instead of all orders. If you use startDate and endDate parameters, the maximum allowable range is two weeks.
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetShipmentPackages>> GetShipmentPackagesAsync(FilterGetShipmentPackages? filter = null) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/orders";

    url = url.BuildUrl(
                       new("startDate", filter?.StartDate),
                       new("endDate", filter?.EndDate),
                       new("page", filter?.Page),
                       new("size", filter?.Size),
                       new("supplierId", filter?.SupplierId),
                       new("orderNumber", filter?.OrderNumber),
                       new("status", filter?.Status),
                       new("orderByField", filter?.OrderByField),
                       new("orderByDirection", filter?.OrderByDirection),
                       new("shipmentPackageIds", filter?.ShipmentPackageIds)
                      );
  
    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetShipmentPackages>();
    return result.WithData(data);
  }


  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/kargo-takip-kodu-bildirme
  ///  <br/><br/>
  /// When this method is called for any package, it is now not through Trendyol's agreement but The status of the shipment made by the supplier on its own agreement. The status of the shipment starts to be questioned and shipped, Delivered information is received and tracked through the integration.
  ///   <br/><br/>
  /// If an order has been cancelled, Get Order Packages service must be used and the Updating Shipping Code must be made to the current package number.
  ///   <br/><br/>
  /// If the package you are trying to feed with the cargo tracking number is in the status of cancelled, shipped, delivered or if the tracking code has been fed to the relevant package before, you will receive the error "Shipment Update Edilebilir Bir Durumda Değil.".
  ///  <br/><br/>
  /// The shipping code cannot be used for packages with the status of shipped, delivered.
  ///  <br/><br/>
  /// If a notification of unsupplied is made to a package whose cargo number has been updated, a new package and cargo code will be created, so it is expected that the cargo code of the new package will be updated again through the integration.
  ///  <br/><br/>
  /// If you are working with Yurtiçi Kargo, the courier code you have forwarded must be processed by YK. Otherwise, you may get an "invalid tracking number"(400 Bad Request) error.
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <param name="trackingNumber"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> UpdateTrackingNumberAsync(string shipmentPackageId, string trackingNumber) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/{shipmentPackageId}/update-tracking-number";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    //Request obj not needed to be modeled 
    var request = new {
      trackingNumber
    };
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/paket-statu-bildirimi
  ///  <br/><br/>
  /// It is used to notify Trendyol of the creation of the invoice of the order package. Notification of Invoice,a reference to prevent customer-originated cancellations reaching to Trendyol Customer Care Service.
  ///  <br/><br/>
  /// You can only update the package of the order with 2 package status. Except for these statuses, the system is automatically transferred to the package. You can find detailed information about the statuses below.
  ///  <br/><br/>
  /// While notifying the status, you should first notify "Picking" and then "Invoiced".
  ///  <br/><br/>
  /// Notify Packages as Picking: As soon as you notify the Picking status, the phrase "Order Processed" will be displayed on the Trendyol panel. With this status, you can check the status of your orders on your side.
  /// <br/><br/>
  /// Notify Packages as Invoiced: As soon as you notify the Invoiced status, the phrase "Order Processed" will be displayed on the Trendyol panel. With this status, you can check the status of your orders on your side.
  /// </summary>
  /// <param name="id"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> UpdatePackageStatusAsync(string id, RequestUpdatePackageStatus request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/shipment-packages/{id}";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/cancel-order-package-item
  ///  <br/><br/>
  /// You can use this method to cancel one or more item in Order Package.
  ///  <br/><br/>
  /// Each order package has a PackageID value. If one of the item is cancelled in the package,the package will be split automatically and new PackageID will be generated and assigned to the new package.
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> UpdatePackageUnsuppliedAsync(string shipmentPackageId, RequestUpdatePackageUnsupplied request) {
    var url = $"https://api.trendyol.com/integration/oms/core/sellers/{_supplierId}/shipment-packages/{shipmentPackageId}/items/unsupplied";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/fatura-linki-gonderme
  /// <br/><br/>
  /// Invoice links sent with this service must be accessible for 8 years as required by law.
  /// If you cannot send the invoice for the relevant order by adding a PDF to the encrypted mail address in the order, you have to use this service.
  /// <br/><br/>
  /// By triggering this method Trendyol sends an e-invoice email to Trendyol Customer including the link provided by Supplier
  /// <br/><br/>
  /// Encrypted mail addresses are not fixed customer-based, unique mail is produced for each order.
  /// <br/><br/>
  /// There are two reasons for the 409 error.
  /// <br/>
  /// An invoice for the submitted ShipmentPackageId may have already been fed.
  /// <br/>
  /// The submitted link may have already been fed to another ShipmentPackageId before.
  /// </summary>
  public async Task<TrendyolApiResult> SendInvoiceLinkAsync(RequestSendInvoiceLink request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/supplier-invoice-links";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/fatura-linki-silme
  ///  <br/><br/>
  /// Invoices that were fed incorrectly before, can be deleted through this service and fed again through the send invoice link service.
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> DeleteInvoiceLinkAsync(string shipmentPackageId) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/supplier-invoice-links/delete";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
  ///  <br/><br/>
  /// Use this method to spilt one or more item in Order Package.
  ///  <br/><br/>
  /// Each order package has a PackageID value. If one of the item is split in the package,the package status will be "Unpack" automatically and new PackageID will be generated and assigned tothe new package. Therefore, you must get orders with this endpoint
  /// Dividing Order Packages with Multiple Barcodes
  ///  <br/><br/>
  ///After using this service, new packages depending on the order number will be generated asynchronously. For this reason, you have to fetch the new generated packages from the getShipmentPackages again.
  ///  <br/><br/>
  /// With this method, you can process the products included in an order package with the quantity and orderLineId value of the relevant barcode in the package.
  ///  <br/><br/>
  /// If there is a product(s) that you exclude while requesting, that product(s) will be created in a separate and new package.
  ///
  ///
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> SplitMultiPackageByQuantityAsync(string shipmentPackageId, RequestSplitMultiPackageByQuantity request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/shipment-packages/{shipmentPackageId}/split-packages";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
  ///  <br/><br/>
  /// Split Order Package Item ( Creating Multiple Packages with a Single Request )
  ///  <br/><br/>
  /// After using this service, new packages depending on the order number will be generated asynchronously. For this reason, you have to fetch the new generated packages from the getShipmentPackages again.
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> SplitShipmentPackageAsync(string shipmentPackageId) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/shipment-packages/{shipmentPackageId}/split";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
  ///  <br/><br/>
  /// In this example, there will be 3 packages; First ,one package for 3,5,6 orderLines in the package. Second, another package for 7,8,9 orderLines, and third, one package for the remaining orderLines.
  ///  <br/><br/>
  /// All orderLines in a package should not be sent to this service. The final package for the remaining orderlines will be created by the system automatically.
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> MultiSplitShipmentPackageAsync(string shipmentPackageId) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/shipment-packages/{shipmentPackageId}/multi-split";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
  ///  <br/><br/>
  /// After using this service, new packages depending on the order number will be generated asynchronously. For this reason, you have to fetch the new generated packages from the getShipmentPackages again.
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> SplitShipmentPackageByQuantityAsync(string shipmentPackageId) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/shipment-packages/{shipmentPackageId}/quantity-split";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/desi-ve-koli-bilgisi-bildirimi
  ///  <br/><br/>
  /// With this service, you can feed deci and box quantity for your order packages of UPS Cargo and CEVA Logistics companies.
  ///  <br/><br/>
  /// "Box Quantity" for UPS Cargo, "Box Quantity" and "deci" are required for CEVA Logistics.
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> UpdateBoxInfoAsync(string shipmentPackageId, RequestUpdateBoxInfo request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/shipment-packages/{shipmentPackageId}/box-info";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/alternatif-teslimat-ile-gonderim
  ///  <br/><br/>
  /// You can forward these transactions to Trendyol with the following services, where alternative shipping options are used to deliver the created order package to the customer.
  ///  <br/><br/>
  /// After receiving the 200 response, you can access the current cargoTrackingNumber value from the order packages picking service and send this value to us after the customer receives the order with this value.
  /// </summary>
  /// <param name="id"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> ProcessAlternativeDeliveryAsync(string id, RequestProcessAlternativeDelivery request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/shipment-packages/{id}/alternative-delivery";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync();
    return result;
  }

  /// <summary>
  /// https://api.trendyol.com/sapigw/suppliers/{supplierId}/manual-deliver/{cargoTrackingNumber}
  ///  <br/><br/>
  /// It doesn't need a JSON body when making this request. After sending the request, you will receive a 200 OK response.
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult> ManualDeliverAsync(string cargoTrackingNumber) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/manual-deliver/{cargoTrackingNumber}";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/shipping-alternative-delivery
  ///  <br/><br/>
  /// You can use this service for orders that are returned to your warehouse in the case of "shipped" status and cannot be delivered to the customer, and therefore cannot be transferred to "delivered" status.
  /// <br/><br/>
  /// It doesn't need a JSON body when making this request. After sending the request, you will receive a 200 OK response.
  /// </summary>
  /// <param name="cargoTrackingNumber"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> ManualReturnAsync(string cargoTrackingNumber) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/manual-return/{cargoTrackingNumber}";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync();
    return result;
  }


  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/yetkili-servis-gonderimi
  ///  Our sellers who ship their products to the authorized service should call this service.
  ///  <br/><br/>
  /// Our sellers who integrate Supplier Pays with logistics companies and ship their products to the authorized service should call this service. (Currently, we only work with Horoz Logistics among the logistics companies, and we have supplier pays.)
  ///  <br/><br/>
  /// This service can be called at any time until the package is migrated to shipped status
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> DeliveredByServiceAsync(string id) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/shipment-packages/{id}/delivered-by-service";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync();
    return result;
  }


  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/paket-kargo-firmasi-degistirme
  ///  <br/><br/>
  /// This service is used to change the cargo provides of packages.
  /// <br/><br/>
  /// Usable cargo providers:
  /// “YKMP”
  /// “ARASMP”
  /// “SURATMP”
  /// “HOROZMP”
  /// “MNGMP”
  /// “PTTMP”
  /// “CEVAMP”
  /// “TEXMP”
  /// "SENDEOMP"
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> ChangeCargoProviderAsync(string shipmentPackageId, RequestChangeCargoProvider request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/shipment-packages/{shipmentPackageId}/cargo-providers";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result;
  }


  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/depo-bilgisi-guncelleme
  ///  <br/><br/>
  /// This service will only be available for our sellers using Trendyol Express. With this service, the "WarehouseId" field returned from the Picking Order Packages service will be updated.
  ///   <br/><br/>
  /// If it is desired to add a shipping address, it can be added via the seller panel.
  /// </summary>
  /// <param name="packageId"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> UpdateWarehouseAsync(string packageId, RequestUpdateWarehouse request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/shipment-packages/{packageId}/warehouse";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result;
  }


  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/additional-supply-time-definition
  ///  <br/><br/>
  /// Through this service, sellers can define additional supply times for order packages. If the agreedDeliveryDateExtendible field returns true, the following service will be available. If the field returns false, an error will be returned to you.
  /// <br/><br/>
  /// There will be a specified date range for additional time to be entered. You can see this date range in order packages picking service as agreedDeliveryExtensionStartDate, agreedDeliveryExtensionEndDate.
  /// <br/><br/>
  ///   If no additional time is entered, the order will be canceled by Trendyol on agreedDeliveryExtensionEndDate.
  /// <br/><br/>
  ///   The following fields have been added to the response body of the order packages service.
  /// <br/>
  /// "agreedDeliveryDateExtendible"<br/>
  /// "extendedAgreedDeliveryDate"<br/>
  /// "agreedDeliveryExtensionStartDate"<br/>
  /// "agreedDeliveryExtensionEndDate"<br/>
  /// </summary>
  /// <param name="packageId"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> AgreedDeliveryDateAsync(string packageId) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/shipment-packages/{packageId}/extended-agreed-delivery-date";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync();
    return result;
  }


  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/address-information
  ///  <br/><br/>
  /// You can get the country information from the below service
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult> GetCountriesAsync() {
    var url = "https://api.trendyol.com/integration/oms/core/countries";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/address-information
  ///  <br/><br/>
  /// You can get the city information for the gulf region from the below service.
  ///  <br/><br/>
  /// CountryCode information should be taken by the field called "code" from Country Information service
  /// </summary>
  /// <param name="countryCode"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> GetCitiesGULFAsync(string countryCode) {
    var url = $"https://api.trendyol.com/integration/oms/core/countries/{countryCode}/cities";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/address-information
  ///  <br/><br/>
  /// You can get the city information for the Azerbaijan from the below service.
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult> GetCitiesAzerbaijanAsync() {
    var url = $"https://api.trendyol.com/integration/oms/core/countries/domestic/AZ/cities";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/address-information
  ///  <br/><br/>
  /// You can get the district information for the Azerbaijan from the below service.
  ///  <br/><br/>
  /// CityCode information should be taken by the field called "id" from City Information service
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult> GetDistrictsAzerbaijanAsync(string cityCode) {
    var url = $"https://api.trendyol.com/integration/oms/core/countries/domestic/AZ/cities/{cityCode}/districts";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    return result;
  }


  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/address-information
  ///  <br/><br/>
  ///  You can get the city information for the Turkey from the below service.
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult> GetCitiesTurkeyAsync() {
    var url = $"https://api.trendyol.com/integration/oms/core/countries/domestic/TR/cities";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/address-information
  ///  <br/><br/>
  /// You can get the district information for the Turkey from the below service.
  ///  <br/><br/>
  /// CityCode information should be taken by the field called "id" from City Information service
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult> GetDistrictsTurkeyAsync(string cityCode) {
    var url = $"https://api.trendyol.com/integration/oms/core/countries/domestic/TR/cities/{cityCode}/districts";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/address-information
  ///  <br/><br/>
  /// You can get the neighborhood information for the Turkey from the below service.
  ///  <br/><br/>
  /// CityCode information should be taken by the field called "id" from City Information service
  ///  <br/><br/>
  /// DistrictCode information should be taken by the field called "id" from District Information service
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult> GetNeighborhoodsTurkeyAsync(string cityCode, string districtCode) {
    var url = $"https://api.trendyol.com/integration/oms/core/countries/domestic/TR/cities/{cityCode}/districts/{districtCode}/neighborhoods";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    return result;
  }


  //TODO: IMPL https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/creating-test-order
  //TODO: IMPL https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/test-order-statu-updates

  #endregion


}