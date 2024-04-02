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


namespace TrendyolSharp.Marketplace;

public class TrendyolMarketplaceClient
{
  private readonly string _supplierId;
  private readonly string _apiKey;
  private readonly string _apiSecret;
  private readonly string _token;
  private readonly ILogger? _logger;


  private readonly HttpClient _httpClient;

  /// <summary>
  ///  Constructor for Trendyol Marketplace Client
  /// </summary>
  /// <param name="supplierId">Trendyol seller API id</param>
  /// <param name="apiKey">Trendyol seller API key</param>
  /// <param name="apiSecret">Trendyol seller API secret</param>
  /// <param name="token">Trendyol seller API token</param>
  /// <param name="integrationCompanyNameForHeader">Set a company name for User-Agent header</param>
  /// <param name="isUseStageApi">Whether to use the stage api or not</param>
  /// <param name="logger">Set logger for trendyol marketplace api client, uses serilog</param>
  public TrendyolMarketplaceClient(string supplierId,
                                   string apiKey,
                                   string apiSecret,
                                   string token,
                                   string integrationCompanyNameForHeader = "SelfIntegration",
                                   bool isUseStageApi = false,
                                   ILogger? logger = null) {
    _supplierId = supplierId;
    _apiKey = apiKey;
    _apiSecret = apiSecret;
    _token = token;
    _logger = logger;
    var baseApiUrl = isUseStageApi
                       ? "https://stageapi.trendyol.com/"
                       : "https://api.trendyol.com/";

    _httpClient = new HttpClient();
    _httpClient.BaseAddress = new Uri(baseApiUrl);
    _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{supplierId} - {integrationCompanyNameForHeader}");
    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"))}");
  }


  #region PRODUCT INTEGRATION

  //LINK: https://developers.trendyol.com/docs/category/%C3%BCr%C3%BCn-entegrasyonu
  //EXAMPLE REQUESTS: https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/ornek-istekler

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/iade-ve-sevkiyat-adres-bilgileri
  /// </summary>
  /// <returns></returns>
  public async Task<ResponseGetSuppliersAddresses> GetSupplierAddressesAsync() {
    var url = $"/sapigw/suppliers/{_supplierId}/addresses";
    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    return result.Content.ToObject<ResponseGetSuppliersAddresses>();
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-marka-listesi
  /// </summary>
  /// <param name="pageRequest"></param>
  /// <returns></returns>
  public async Task<ResponseGetBrands> GetBrandsAsync(FilterPage? pageRequest = null) {
    var url = "/sapigw/brands";
    if (pageRequest is not null) {
      //for each prop if not null add to url
      url += $"&page={pageRequest.Page}";
      url += $"&size={pageRequest.Size}";
    }

    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    return result.Content.ToObject<ResponseGetBrands>();
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-marka-listesi
  /// </summary>
  /// <param name="pageRequest"></param>
  /// <returns></returns>
  public async Task<ResponseGetBrandsByName> GetBrandsByNameAsync(string name) {
    var url = $"/sapigw/brands/by-name&name={name}";
    if (string.IsNullOrEmpty(name)) {
      throw new ArgumentNullException(nameof(name));
    }

    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    return result.Content.ToObject<ResponseGetBrandsByName>();
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-kategori-listesi
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public async Task<ResponseGetCategoryTree> GetCategoryTree(FilterGetCategoryTree? filter = null) {
    var url = "/sapigw/product-categories";
    if (filter is not null) {
      //for each prop if not null add to url
      if (filter.Id.HasValue) {
        url += $"&id={filter.Id}";
      }

      if (filter.ParentId.HasValue) {
        url += $"&parentId={filter.ParentId}";
      }

      if (!string.IsNullOrEmpty(filter.Name)) {
        url += $"&name={filter.Name}";
      }

      if (filter.SubCategories.HasValue) {
        url += $"&subCategories={filter.SubCategories}";
      }
    }

    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    return result.Content.ToObject<ResponseGetCategoryTree>();
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-kategori-ozellik-listesi
  /// </summary>
  /// <param name="categoryId"></param>
  /// <param name="filter"></param>
  /// <returns></returns>
  public async Task<ResponseGetCategoryAttributes> GetCategoryAttributesAsync(int categoryId, FilterGetCategoryAttributes? filter = null) {
    var url = $"/sapigw/product-categories/{categoryId}/attributes";
    if (filter is not null) {
      //for each prop if not null add to url
      if (!string.IsNullOrEmpty(filter.Name)) {
        url += $"&name={filter.Name}";
      }

      if (!string.IsNullOrEmpty(filter.DisplayName)) {
        url += $"&displayName={filter.DisplayName}";
      }

      if (filter.AttributeId.HasValue) {
        url += $"&attributeId={filter.AttributeId}";
      }

      if (!string.IsNullOrEmpty(filter.AttributeName)) {
        url += $"&attributeName={filter.AttributeName}";
      }

      if (filter.AllowCustom.HasValue) {
        url += $"&allowCustom={filter.AllowCustom}";
      }

      if (filter.Required.HasValue) {
        url += $"&required={filter.Required}";
      }

      if (filter.Slicer.HasValue) {
        url += $"&slicer={filter.Slicer}";
      }

      if (filter.Varianter.HasValue) {
        url += $"&varianter={filter.Varianter}";
      }

      if (filter.AttributeValueId.HasValue) {
        url += $"&attributeValueId={filter.AttributeValueId}";
      }

      if (!string.IsNullOrEmpty(filter.AttributeValueName)) {
        url += $"&attributeValueName={filter.AttributeValueName}";
      }
    }

    var request = new TrendyolRequest(_httpClient, url.Replace("{categoriId}", categoryId.ToString()));
    var result = await request.SendGetRequestAsync();
    return result.Content.ToObject<ResponseGetCategoryAttributes>();
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-aktarma-v2
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<ResponseBatchRequestId> CreateProductsAsync(RequestCreateProducts request) {
    var url = $"/sapigw/suppliers/{_supplierId}/v2/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    return result.Content.ToObject<ResponseBatchRequestId>();
  }


  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-urun-bilgisi-guncelleme
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<ResponseBatchRequestId> UpdateProductsAsync(RequestUpdateProduct request) {
    var url = $"/sapigw/suppliers/{_supplierId}/v2/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result.Content.ToObject<ResponseBatchRequestId>();
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/stok-ve-fiyat-guncelleme
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<ResponseBatchRequestId> UpdatePriceAndInventoryAsync(RequestUpdatePriceAndInventory request) {
    var url = $"/sapigw/suppliers/{_supplierId}/products/price-and-inventory";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    return result.Content.ToObject<ResponseBatchRequestId>();
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-silme
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<ResponseBatchRequestId> DeleteProductAsync(RequestDeleteProducts request) {
    var url = $"/sapigw/suppliers/{_supplierId}/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendDeleteRequestAsync();
    return result.Content.ToObject<ResponseBatchRequestId>();
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/toplu-islem-kontrolu
  /// </summary>
  /// <param name="batchRequestId"></param>
  /// <returns></returns>
  public async Task<ResponseGetBatchRequestResult> GetBatchRequestResultAsync(string batchRequestId) {
    var url = $"/sapigw/suppliers/{_supplierId}/products/batch-requests/{batchRequestId}";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    return result.Content.ToObject<ResponseGetBatchRequestResult>();
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-filtreleme
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public async Task<ResponseGetProductsFiltered> GetProducts(FilterProducts? filter = null) {
    //TODO: Check if can take all products without filtering
    var url = $"/sapigw/suppliers/{_supplierId}/products";
    if (filter is not null) {
      if (filter.Approved.HasValue) {
        url += $"&approved={filter.Approved}";
      }

      if (!string.IsNullOrEmpty(filter.Barcode)) {
        url += $"&barcode={filter.Barcode}";
      }

      if (filter.StartDate.HasValue) {
        url += $"&startDate={filter.StartDate}";
      }

      if (filter.EndDate.HasValue) {
        url += $"&endDate={filter.EndDate}";
      }

      if (filter.Page.HasValue) {
        url += $"&page={filter.Page}";
      }

      if (!string.IsNullOrEmpty(filter.DateQueryType)) {
        url += $"&dateQueryType={filter.DateQueryType}";
      }

      if (filter.Size.HasValue) {
        url += $"&size={filter.Size}";
      }

      if (filter.SupplierId.HasValue) {
        url += $"&supplierId={filter.SupplierId}";
      }

      if (!string.IsNullOrEmpty(filter.StockCode)) {
        url += $"&stockCode={filter.StockCode}";
      }

      if (filter.Archived.HasValue) {
        url += $"&archived={filter.Archived}";
      }

      if (!string.IsNullOrEmpty(filter.ProductMainId)) {
        url += $"&productMainId={filter.ProductMainId}";
      }

      if (filter.OnSale.HasValue) {
        url += $"&onSale={filter.OnSale}";
      }

      if (filter.Rejected.HasValue) {
        url += $"&rejected={filter.Rejected}";
      }

      if (filter.Blacklisted.HasValue) {
        url += $"&blacklisted={filter.Blacklisted}";
      }

      if (filter.BrandIds is not null) {
        foreach (var brandId in filter.BrandIds) {
          url += $"&brandIds={brandId}";
        }
      }
    }

    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    return result.Content.ToObject<ResponseGetProductsFiltered>();
  }

  #endregion

  #region ORDER INTEGRATION

  //LINK: https://developers.trendyol.com/docs/category/sipari%C5%9F-entegrasyonu
  //CREATE TEST PRODUCT: https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/test-siparisi-olusturma

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-cekme
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/askidaki-siparis-paketlerini-cekme
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public async Task<ResponseGetShipmentPackages> GetShipmentPackages(FilterGetShipmentPackages? filter = null) {
    var url = $"/sapigw/suppliers/{_supplierId}/orders";
    if (filter is not null) {
      if (filter.StartDate.HasValue) {
        url += $"&startDate={filter.StartDate}";
      }

      if (filter.EndDate.HasValue) {
        url += $"&endDate={filter.EndDate}";
      }

      if (filter.Page.HasValue) {
        url += $"&page={filter.Page}";
      }

      if (filter.Size.HasValue) {
        url += $"&size={filter.Size}";
      }

      if (filter.SupplierId.HasValue) {
        url += $"&supplierId={filter.SupplierId}";
      }

      if (!string.IsNullOrEmpty(filter.OrderNumber)) {
        url += $"&orderNumber={filter.OrderNumber}";
      }

      if (!string.IsNullOrEmpty(filter.Status)) {
        url += $"&status={filter.Status}";
      }

      if (!string.IsNullOrEmpty(filter.OrderByField)) {
        url += $"&orderByField={filter.OrderByField}";
      }

      if (!string.IsNullOrEmpty(filter.OrderByDirection)) {
        url += $"&orderByDirection={filter.OrderByDirection}";
      }

      if (filter.ShipmentPackageIds is not null) {
        foreach (var shipmentPackageId in filter.ShipmentPackageIds) {
          url += $"&shipmentPackageIds={shipmentPackageId}";
        }
      }
    }

    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    return result.Content.ToObject<ResponseGetShipmentPackages>();
  }


  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/kargo-takip-kodu-bildirme
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <param name="trackingNumber"></param>
  /// <returns></returns>
  public async Task<ResponseInformation> UpdateTrackingNumber(string shipmentPackageId, string trackingNumber) {
    var url = $"/sapigw/suppliers/{_supplierId}/{shipmentPackageId}/update-tracking-number";
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
  /// </summary>
  /// <param name="id"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<ResponseInformation> UpdatePackageStatusAsync(string id, RequestUpdatePackageStatus request) {
    var url = $"/sapigw/suppliers/{_supplierId}/shipment-packages/{id}";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result;
  }

  public async Task<ResponseInformation> UpdatePackageUnsuppliedAsync(string shipmentPackageId, RequestUpdatePackageUnsupplied request) {
    var url = $"/integration/oms/core/sellers/{_supplierId}/shipment-packages/{shipmentPackageId}/items/unsupplied";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/fatura-linki-gonderme
  /// <br/><br/>
  /// Bu servis ile gönderilen fatura bağlantılarının hukuki zorunluluk gereği 8 yıl boyunca erişilebilir durumda olması gereklidir.
  /// <br/><br/>
  /// 409 hatasının iki sebebi bulunmaktadır.
  /// <br/>
  /// Gönderilen ShipmentPackageId'ye ait bir fatura zaten beslenmiş olabilir.
  /// <br/>
  /// Gönderilen link daha önce başka bir ShipmentPackageId'ye zaten beslenmiş olabilir.
  /// </summary>
  public async Task<ResponseInformation> SendInvoiceLinkAsync(RequestSendInvoiceLink request) {
    var url = $"/sapigw/suppliers/{_supplierId}/supplier-invoice-links";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/fatura-linki-silme
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <returns></returns>
  public async Task<ResponseInformation> DeleteInvoiceLinkAsync(string shipmentPackageId) {
    var url = $"/sapigw/suppliers/{_supplierId}/supplier-invoice-links/delete";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<ResponseInformation> SplitMultiPackageByQuantityAsync(string shipmentPackageId, RequestSplitMultiPackageByQuantity request) {
    var url = $"/sapigw/suppliers/{_supplierId}/shipment-packages/{shipmentPackageId}/split-packages";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <returns></returns>
  public async Task<ResponseInformation> SplitShipmentPackageAsync(string shipmentPackageId) {
    var url = $"/sapigw/suppliers/{_supplierId}/shipment-packages/{shipmentPackageId}/split";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <returns></returns>
  public async Task<ResponseInformation> MultiSplitShipmentPackageAsync(string shipmentPackageId) {
    var url = $"/sapigw/suppliers/{_supplierId}/shipment-packages/{shipmentPackageId}/multi-split";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <returns></returns>
  public async Task<ResponseInformation> SplitShipmentPackageByQuantityAsync(string shipmentPackageId) {
    var url = $"/sapigw/suppliers/{_supplierId}/shipment-packages/{shipmentPackageId}/quantity-split";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync();
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/desi-ve-koli-bilgisi-bildirimi
  /// </summary>
  /// <param name="shipmentPackageId"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<ResponseInformation> UpdateBoxInfoAsync(string shipmentPackageId, RequestUpdateBoxInfo request) {
    var url = $"/sapigw/suppliers/{_supplierId}/shipment-packages/{shipmentPackageId}/box-info";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/alternatif-teslimat-ile-gonderim
  ///
  /// You can also use this to send verify digital products delivery. Add digitalCode to params in request model.
  /// </summary>
  /// <param name="id"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<ResponseInformation> ProcessAlternativeDeliveryAsync(string id, RequestProcessAlternativeDelivery request) {
    var url = $"/sapigw/suppliers/{_supplierId}/shipment-packages/{id}/alternative-delivery";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync();
    return result;
  }

  /// <summary>
  /// https://api.trendyol.com/sapigw/suppliers/{supplierId}/manual-deliver/{cargoTrackingNumber}
  /// </summary>
  /// <returns></returns>
  public async Task<ResponseInformation> ManualDeliverAsync(string cargoTrackingNumber) {
    var url = $"/sapigw/suppliers/{_supplierId}/manual-deliver/{cargoTrackingNumber}";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync();
    return result;
  }


  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/yetkili-servis-gonderimi
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public async Task<ResponseInformation> DeliveredByServiceAsync(string id) {
    var url = $"/sapigw/suppliers/{_supplierId}/shipment-packages/{id}/delivered-by-service";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync();
    return result;
  }


  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/paket-kargo-firmasi-degistirme
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
  public async Task<ResponseInformation> ChangeCargoProviderAsync(string shipmentPackageId, RequestChangeCargoProvider request) {
    var url = $"/sapigw/suppliers/{_supplierId}/shipment-packages/{shipmentPackageId}/cargo-providers";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result;
  }

  
  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/depo-bilgisi-guncelleme
  /// </summary>
  /// <param name="packageId"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<ResponseInformation> UpdateWarehouseAsync(string packageId, RequestUpdateWarehouse request) {
    var url = $"/sapigw/suppliers/{_supplierId}/shipment-packages/{packageId}/warehouse";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result;
  }

  
  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/order-integration/additional-supply-time-definition
  /// </summary>
  /// <param name="packageId"></param>
  /// <returns></returns>
  public async Task<ResponseInformation> AgreedDeliveryDateAsync(string packageId) {
    var url = $"/sapigw/suppliers/{_supplierId}/shipment-packages/{packageId}/extended-agreed-delivery-date";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync();
    return result;
  }
  
  

  #endregion
}