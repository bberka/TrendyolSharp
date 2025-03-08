using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendyolClient.Sharp.Extensions;
using TrendyolClient.Sharp.Models;
using TrendyolClient.Sharp.Models.Marketplace.Filter;
using TrendyolClient.Sharp.Models.Marketplace.Request;
using TrendyolClient.Sharp.Models.Marketplace.Response;
using TrendyolClient.Sharp.Utils;

namespace TrendyolClient.Sharp.Services.Marketplace
{
  //TODO: IMPL https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/test-siparisi-olusturma
  //TODO: https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/test-siparisi-stat%C3%BC-update

  public partial class TrendyolMarketplaceClient
  {
    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-cekme
    /// </summary>
    public async Task<TrendyolApiResult<ResponseGetShipmentPackages>> GetShipmentPackagesAsync(FilterGetShipmentPackages filter = null) {
      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/orders";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/orders";
      var url = UseStageApi
                  ? stage
                  : prod;

      url = url.BuildUrl(
                         new Dictionary<string, object> {
                           { "startDate", filter?.StartDate },
                           { "endDate", filter?.EndDate },
                           { "page", filter?.Page },
                           { "size", filter?.Size },
                           { "supplierId", filter?.SupplierId },
                           { "orderNumber", filter?.OrderNumber },
                           { "status", filter?.Status },
                           { "orderByField", filter?.OrderByField },
                           { "orderByDirection", filter?.OrderByDirection },
                           { "shipmentPackageIds", filter?.ShipmentPackageIds }
                         }
                        );
      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.JsonToObject<ResponseGetShipmentPackages>();
      return result.WithData(data);
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/kargo-takip-kodu-bildirme
    /// </summary>
    public async Task<TrendyolApiResult> UpdateTrackingNumberAsync(string shipmentPackageId, RequestUpdateTrackingNumber request) {
      if (string.IsNullOrEmpty(shipmentPackageId)) throw new ArgumentNullException(nameof(shipmentPackageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{shipmentPackageId}/update-tracking-number";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{shipmentPackageId}/update-tracking-number";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPutRequestAsync(request);
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/paket-statu-bildirimi
    /// </summary>
    public async Task<TrendyolApiResult> UpdatePackageStatusAsync(string packageId, RequestUpdatePackageStatus request) {
      if (string.IsNullOrEmpty(packageId)) throw new ArgumentNullException(nameof(packageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPutRequestAsync(request);
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/tedarik-edememe-bildirimi
    /// </summary>
    public async Task<TrendyolApiResult> UpdatePackageUnsuppliedAsync(string packageId, RequestUpdatePackageUnsupplied request) {
      if (string.IsNullOrEmpty(packageId)) throw new ArgumentNullException(nameof(packageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/items/unsupplied";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/items/unsupplied";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPutRequestAsync(request);
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/fatura-linki-gonderme
    /// </summary>
    public async Task<TrendyolApiResult> SendInvoiceLinkAsync(RequestSendInvoiceLink request) {
      var prod = $"https://apigw.trendyol.com/integration/sellers/{SellerId}/seller-invoice-links";
      var stage = $"https://stageapigw.trendyol.com/integration/sellers/{SellerId}/seller-invoice-links";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPostRequestAsync(request);
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/fatura-linki-silme
    ///   <br /><br />
    ///   Invoices that were fed incorrectly before, can be deleted through this service and fed again through the send invoice
    ///   link service.
    /// </summary>
    public async Task<TrendyolApiResult> DeleteInvoiceLinkAsync(RequestDeleteInvoiceLink request) {
      var prod = $"https://apigw.trendyol.com/integration/sellers/{SellerId}/seller-invoice-links/delete";
      var stage = $"https://stageapigw.trendyol.com/integration/sellers/{SellerId}/seller-invoice-links/delete";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPostRequestAsync(request);
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
    /// </summary>
    public async Task<TrendyolApiResult> SplitMultiPackageByQuantityAsync(string packageId,
                                                                          RequestSplitMultiPackageByQuantity request) {
      if (string.IsNullOrEmpty(packageId)) throw new ArgumentNullException(nameof(packageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/split-packages";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/split-packages";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPostRequestAsync(request);
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
    /// </summary>
    public async Task<TrendyolApiResult> SplitShipmentPackageAsync(string packageId) {
      if (string.IsNullOrEmpty(packageId)) throw new ArgumentNullException(nameof(packageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/split";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/split";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPostRequestAsync();
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
    /// </summary>
    public async Task<TrendyolApiResult> MultiSplitShipmentPackageAsync(string packageId) {
      if (string.IsNullOrEmpty(packageId)) throw new ArgumentNullException(nameof(packageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/multi-split";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/multi-split";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPostRequestAsync();
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
    /// </summary>
    public async Task<TrendyolApiResult> SplitShipmentPackageByQuantityAsync(string packageId) {
      if (string.IsNullOrEmpty(packageId)) throw new ArgumentNullException(nameof(packageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/quantity-split";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/quantity-split";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPostRequestAsync();
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/desi-ve-koli-bilgisi-bildirimi
    /// </summary>
    public async Task<TrendyolApiResult> UpdateBoxInfoAsync(string packageId, RequestUpdateBoxInfo request) {
      if (string.IsNullOrEmpty(packageId)) throw new ArgumentNullException(nameof(packageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/box-info";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/box-info";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPutRequestAsync(request);
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/alternatif-teslimat-ile-gonderim
    /// </summary>
    public async Task<TrendyolApiResult> ProcessAlternativeDeliveryAsync(string packageId, RequestProcessAlternativeDelivery request) {
      if (string.IsNullOrEmpty(packageId)) throw new ArgumentNullException(nameof(packageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/alternative-delivery";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/alternative-delivery";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPutRequestAsync(request);
      return result;
    }

    /// <summary>
    ///   https://api.trendyol.com/sapigw/suppliers/{supplierId}/manual-deliver/{cargoTrackingNumber}
    /// </summary>
    public async Task<TrendyolApiResult> ManualDeliverAsync(string cargoTrackingNumber) {
      if (string.IsNullOrEmpty(cargoTrackingNumber)) throw new ArgumentNullException(nameof(cargoTrackingNumber));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/manual-deliver/{cargoTrackingNumber}";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/manual-deliver/{cargoTrackingNumber}";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPutRequestAsync();
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/alternatif-teslimat-ile-gonderim
    /// </summary>
    public async Task<TrendyolApiResult> ManualReturnAsync(string cargoTrackingNumber) {
      if (string.IsNullOrEmpty(cargoTrackingNumber)) throw new ArgumentNullException(nameof(cargoTrackingNumber));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/manual-return/{cargoTrackingNumber}";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/manual-return/{cargoTrackingNumber}";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPutRequestAsync();
      return result;
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/yetkili-servis-gonderimi
    /// </summary>
    public async Task<TrendyolApiResult> DeliveredByServiceAsync(string packageId) {
      if (string.IsNullOrEmpty(packageId)) throw new ArgumentNullException(nameof(packageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/delivered-by-service";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/delivered-by-service";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPutRequestAsync();
      return result;
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/paket-kargo-firmasi-degistirme
    ///   Usable cargo providers:
    ///   “YKMP”
    ///   “ARASMP”
    ///   “SURATMP”
    ///   “HOROZMP”
    ///   “MNGMP”
    ///   “PTTMP”
    ///   “CEVAMP”
    ///   “TEXMP”
    ///   "KOLAYGELSINMP"
    /// </summary>
    public async Task<TrendyolApiResult> ChangeCargoProviderAsync(string packageId, RequestChangeCargoProvider request) {
      if (string.IsNullOrEmpty(packageId)) throw new ArgumentNullException(nameof(packageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/cargo-providers";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/cargo-providers";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPutRequestAsync(request);
      return result;
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/depo-bilgisi-guncelleme
    /// </summary>
    public async Task<TrendyolApiResult> UpdateWarehouseAsync(string packageId, RequestUpdateWarehouse request) {
      if (string.IsNullOrEmpty(packageId)) throw new ArgumentNullException(nameof(packageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/warehouse";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/warehouse";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPutRequestAsync(request);
      return result;
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/ek-tedarik-s%C3%BCresi-tan%C4%B1mlama
    /// </summary>
    public async Task<TrendyolApiResult> AgreedDeliveryDateAsync(string packageId, RequestAgreedDeliveryDate request) {
      if (string.IsNullOrEmpty(packageId)) throw new ArgumentNullException(nameof(packageId));

      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/extended-agreed-delivery-date";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/shipment-packages/{packageId}/extended-agreed-delivery-date";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPutRequestAsync(request);
      return result;
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
    /// </summary>
    public async Task<TrendyolApiResult> GetCountriesAsync() {
      var prod = "https://apigw.trendyol.com/integration/member/countries";
      var stage = "https://stageapigw.trendyol.com/integration/member/countries";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendGetRequestAsync();
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
    /// </summary>
    public async Task<TrendyolApiResult> GetCitiesGULFAsync(string countryCode) {
      if (string.IsNullOrEmpty(countryCode)) throw new ArgumentNullException(nameof(countryCode));

      var prod = $"https://apigw.trendyol.com/integration/member/countries/{countryCode}/cities";
      var stage = $"https://stageapigw.trendyol.com/integration/member/countries/{countryCode}/cities";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendGetRequestAsync();
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
    /// </summary>
    public async Task<TrendyolApiResult> GetCitiesAzerbaijanAsync() {
      var prod = "https://apigw.trendyol.com/integration/member/countries/domestic/AZ/cities";
      var stage = "https://stageapigw.trendyol.com/integration/member/countries/domestic/AZ/cities";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendGetRequestAsync();
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
    /// </summary>
    public async Task<TrendyolApiResult> GetDistrictsAzerbaijanAsync(string cityCode) {
      if (string.IsNullOrEmpty(cityCode)) throw new ArgumentNullException(nameof(cityCode));

      var prod = $"https://apigw.trendyol.com/integration/member/countries/domestic/AZ/cities/{cityCode}/districts";
      var stage = $"https://stageapigw.trendyol.com/integration/member/countries/domestic/AZ/cities/{cityCode}/districts";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendGetRequestAsync();
      return result;
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
    /// </summary>
    public async Task<TrendyolApiResult> GetCitiesTurkeyAsync() {
      var prod = "https://apigw.trendyol.com/integration/member/countries/domestic/TR/cities";
      var stage = "https://stageapigw.trendyol.com/integration/member/countries/domestic/TR/cities";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendGetRequestAsync();
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
    /// </summary>
    public async Task<TrendyolApiResult> GetDistrictsTurkeyAsync(string cityCode) {
      if (string.IsNullOrEmpty(cityCode)) throw new ArgumentNullException(nameof(cityCode));

      var prod = $"https://apigw.trendyol.com/integration/member/countries/domestic/TR/cities/{cityCode}/districts";
      var stage = $"https://stageapigw.trendyol.com/integration/member/countries/domestic/TR/cities/{cityCode}/districts";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendGetRequestAsync();
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
    /// </summary>
    public async Task<TrendyolApiResult> GetNeighborhoodsTurkeyAsync(string cityCode, string districtCode) {
      if (string.IsNullOrEmpty(cityCode)) throw new ArgumentNullException(nameof(cityCode));

      if (string.IsNullOrEmpty(districtCode)) throw new ArgumentNullException(nameof(districtCode));

      var prod = $"https://apigw.trendyol.com/integration/member/countries/domestic/TR/cities/{cityCode}/districts/{districtCode}/neighborhoods";
      var stage = $"https://stageapigw.trendyol.com/integration/member/countries/domestic/TR/cities/{cityCode}/districts/{districtCode}/neighborhoods";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendGetRequestAsync();
      return result;
    }
  }
}