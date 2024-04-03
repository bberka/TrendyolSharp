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
  private readonly bool _isUseStageApi;
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
    _isUseStageApi = isUseStageApi;
    _logger = logger;
    _httpClient = new HttpClient();
    _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{supplierId} - {integrationCompanyNameForHeader}");
    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"))}");
  }


  #region PRODUCT INTEGRATION

  //LINK: https://developers.trendyol.com/docs/category/%C3%BCr%C3%BCn-entegrasyonu
  //EXAMPLE REQUESTS: https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/ornek-istekler

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/iade-ve-sevkiyat-adres-bilgileri
  /// <br/><br/>
  /// In requests to createProduct V2 service, the order and shipping company information to be sent and the ID values ​​of this information will be obtained using this service.
  ///<br/><br/>
  ///If "SELLER APPLICATION PROCESS" is not fully completed, you should not use this service.
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetSuppliersAddresses>> GetSupplierAddressesAsync() {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/addresses";
    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetSuppliersAddresses>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-marka-listesi
  ///  <br/><br/>
  ///  The brandId information to be sent to the createProduct V2 service will be obtained using this service.
  ///  <br/><br/>
  /// Minumum 1000 brands information can be provided on a page.
  ///   When searching for a brand, you need to create a query using the page parameter to the service.
  /// </summary>
  /// <param name="pageRequest"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetBrands>> GetBrandsAsync(FilterPage? pageRequest = null) {
    var url = "https://api.trendyol.com/sapigw/brands";
    if (pageRequest is not null) {
      //for each prop if not null add to url
      url += $"&page={pageRequest.Page}";
      url += $"&size={pageRequest.Size}";
    }

    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetBrands>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-marka-listesi
  ///  <br/><br/>
  /// Brand names are case sensitive.
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetBrandsByName>> GetBrandsByNameAsync(string name) {
    var url = $"https://api.trendyol.com/sapigw/brands/by-name&name={name}";
    if (string.IsNullOrEmpty(name)) {
      throw new ArgumentNullException(nameof(name));
    }

    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetBrandsByName>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-kategori-listesi
  ///  <br/><br/>
  ///  The categoryId information to be sent in requests to the createProduct V2 service will be obtained using this service.
  ///  <br/><br/>
  ///  Category ID information should be used at the lowest level ("subCategories": []) to create createProduct. If there are subcategories of the category you select, you cannot transfer products with this category.
  /// <br/><br/>
  /// We recommend that you get the updated category list weekly, as new categories may be added
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetCategoryTree>> GetCategoryTree(FilterGetCategoryTree? filter = null) {
    var url = "https://api.trendyol.com/sapigw/product-categories";
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
    var data = result.Content.ToObject<ResponseGetCategoryTree>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-kategori-ozellik-listesi
  ///  <br/><br/>
  /// The attributes information to be sent in requests to the createProduct V2 service and the details of this information will be obtained using this service.
  ///  <br/><br/>
  /// Category ID information should be used at the lowest level ("subCategories": []) to create createProduct. If there are subcategories of the category you select, you cannot transfer products with this category.
  /// <br/><br/>
  /// We recommend that you get the updated category attribute list on a weekly basis, as new category attributes can be added.
  /// </summary>
  /// <param name="categoryId"></param>
  /// <param name="filter"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetCategoryAttributes>> GetCategoryAttributesAsync(int categoryId, FilterGetCategoryAttributes? filter = null) {
    var url = $"https://api.trendyol.com/sapigw/product-categories/{categoryId}/attributes";
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
    var data = result.Content.ToObject<ResponseGetCategoryAttributes>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-aktarma-v2
  ///  <br/><br/>
  /// This method is used when uploading your products to the Trendyol system. It supports single and multiple product shipment.
  ///  <br/><br/>
  /// You must determine the prices of your products in Turkish Lira. Exchange rate information is not supported.
  /// <br/>
  /// Before transferring the product with this method, relevant details should be obtained from Trendyol Brand List and Category/Category Attribute Information services.
  /// <br/>
  ///
  /// The maximum number of items that can be sent in each request is 1000.
  /// <br/>
  /// In order to be able to define in the fastDeliveryType field, the deliveryDuration field must be entered as 1.
  /// <br/>
  /// "stockCode" in product data, equals to "merchantSku" in order data. It can be checked in getShipmentPackages service.
  ///  <br/> <br/>
  /// After the product create process, you need to check the status of your products and the transfer process via the getBatchRequestResult service with the batchRequestId in the response.
  /// <br/> <br/>
  ///Service Responses
  /// <br/>
  ///200	The request was successful. you need to check the status of your products and the transfer process via the getBatchRequestResult service with the batchRequestId in the response.
  /// <br/>
  ///400	Missing or incorrect parameter is used in the URL. Review the document again.
  /// <br/>
  ///401	One of the supplierID, API Key, API Secure Key information you used while sending the request is missing or incorrect. You can find the right information for your store on Trendyol Seller Panel.
  /// <br/>
  ///404	The request url information is incorrect. Review the document again.
  /// <br/>
  ///500	There may have been a momentary error. In case the situation does not improve by waiting a few minutes, create a request under the heading "API Integration Support Request" with the endpoint used, the sent request and the
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseBatchRequestId>> CreateProductsAsync(RequestCreateProducts request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/v2/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    var data = result.Content.ToObject<ResponseBatchRequestId>();
    return result.WithData(data);
  }


  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-urun-bilgisi-guncelleme
  ///  <br/><br/>
  /// This method allows you to update the products that you have previously created using the createProduct V2 service in your Trendyol store.
  ///  <br/><br/>
  /// This service is specifically designed for updating product information only. If you need to update stock and price values, you should use the updatePriceAndInventory service.
  /// <br/><br/>
  /// As new category and category attribute values may be added, it is recommended to check the getCategoryTree and getCategoryAttributes services to ensure that the category and category attribute values you use are up-to-date before updating your products.
  ///  <br/><br/>
  /// The status of the product will be set to "İçerik Kontrol Bekleniyor" (Content Check Pending) after the update request. Your products may still be open for sale. If you don't want to receive orders during this period, it's crucial to update your stock and price information accordingly.
  ///  <br/><br/>
  /// You can include a maximum of 1000 items in each update request.
  ///  <br/><br/>
  /// Note that the productMainId value cannot be updated for approved products.
  ///  <br/><br/>
  /// After the product update process, you need to check the status of your products and the transfer process via the getBatchRequestResult service with the batchRequestId in the response.
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseBatchRequestId>> UpdateProductsAsync(RequestUpdateProduct request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/v2/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    var data = result.Content.ToObject<ResponseBatchRequestId>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/stok-ve-fiyat-guncelleme
  ///  <br/><br/>
  /// The price and stock information of the products created and approved to Trendyol can be updated at the same time.
  ///  <br/><br/>
  /// If you send the same request again without making any changes in the request body in the stock-price update process, an error message will be returned to you. You will see the error message "15 dakika boyunca aynı isteği tekrarlı olarak atamazsınız!". You just need to fix your systems so that you can send only your changing stock-prices.
  ///  <br/><br/>
  ///   The stock you submit in the Quantity field is the salable stock information. Sellable stock information is updated when an order is received or restocked by you.
  ///  <br/><br/>
  ///   You can update a maximum of 1000 items (sku) in stock-price update transactions.
  ///  <br/><br/>
  /// After the product stock and price update process, you need to check the status of your products and the transfer process via the getBatchRequestResult service with the batchRequestId in the response.
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseBatchRequestId>> UpdatePriceAndInventoryAsync(RequestUpdatePriceAndInventory request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/products/price-and-inventory";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    var data = result.Content.ToObject<ResponseBatchRequestId>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-silme
  ///  <br/><br/>
  /// This method is used when removing your products from the Trendyol system. It supports single and multiple product deletion. You can delete your products pending approval and your approved products found in the archive for more than one day and not stopped for sale by Trendyol.
  ///  <br/><br/>
  /// After deleting the product, you need to check the status of your transaction with the batchRequestId in the response via the getBatchRequestResult service.
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseBatchRequestId>> DeleteProductAsync(RequestDeleteProducts request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendDeleteRequestAsync();
    var data = result.Content.ToObject<ResponseBatchRequestId>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/toplu-islem-kontrolu
  ///  <br/><br/>
  /// While createProducts, updatePriceAndInventory methods are processed by the queue in TrendyolSystem, a batchRequestId information is returned in each successful request result. By checking the "status" field in the return of the service, you can check whether the batch has been completed. If more than one item in the batch results in an error, the failureReasons field can be checked to see error reason.
  /// <br/><br/>
  /// You can see the batch request results' createProducts,updateProducts and updatePriceAndInventory services up to 4 hours later.
  ///  <br/><br/>
  ///  After the Stock Price update processes, you need to check the item-based status fields for the batchId you are querying. Batch status field will not return to you.
  /// </summary>
  /// <param name="batchRequestId"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetBatchRequestResult>> GetBatchRequestResultAsync(string batchRequestId) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/products/batch-requests/{batchRequestId}";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetBatchRequestResult>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-filtreleme
  ///  <br/><br/>
  /// With this service, you can list your products in your Trendyol store.
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetProductsFiltered>> GetProducts(FilterProducts? filter = null) {
    //TODO: Check if can take all products without filtering
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/products";
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
    var data = result.Content.ToObject<ResponseGetProductsFiltered>();
    return result.WithData(data);
  }

  #endregion

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
  public async Task<TrendyolApiResult<ResponseGetShipmentPackages>> GetShipmentPackages(FilterGetShipmentPackages? filter = null) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/orders";
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
  public async Task<TrendyolApiResult> UpdateTrackingNumber(string shipmentPackageId, string trackingNumber) {
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

  #region COMMON LABEL INTEGRATION

  //DOC: https://developers.trendyol.com/en/docs/trendyol-marketplace/common-label-integration/common-label-integration

  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/common-label-integration/barcode-request
  /// <br/>
  /// <br/>
  /// The common label process can be used for TEX, Aras Kargo and UPS by supplier paid cargo model.
  /// <br/><br/>
  /// With this service, you can request a barcode for the relevant cargo number for orders in the common label process. After the barcode is created, it will return to you from the service in ZPL format.
  /// <br/><br/>
  /// It is recommended to make the barcode request after picking or feeding invoiced status to the relevant order package.
  /// As a format, there is only the ZPL format available for now.
  /// <br/><br/>
  /// 200	Success - No response body
  /// <br/>
  /// 400	Error - Check the cargoTrackingNumber value you have forwarded
  /// <br/>
  /// 401	Error - You need to check the API information you entered
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult> CreateCommonLabelAsync(string cargoTrackingNumber, RequestCreateCommonLabel request) {
    var url = $"https://sellerpublic-sdc.trendyol.com/delivery-delivery-external-service/sellers/{_apiSecret}/common-label/{cargoTrackingNumber}";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    return result;
  }

  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/common-label-integration/getting-created-common-label
  ///  <br/> <br/>
  /// The common label process can be used for TEX, Aras Kargo and UPS by supplier paid cargo model.
  ///  <br/> <br/>
  /// With this service, you can get the value in the format you created the barcode request.
  ///  <br/> <br/>
  ///   Multiple labels will be returned to you for multiple parcels. In case of a single parcel, one ZPL will be returned.
  /// <br/> <br/>
  /// 400	Error - Check the cargoTrackingNumber value you have forwarded
  ///  <br/>
  /// 401	Error - You need to check the API information you entered
  /// </summary>
  /// <param name="cargoTrackingNumber"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult> GetCommonLabelAsync(string cargoTrackingNumber) {
    var url = $"https://sellerpublic-sdc.trendyol.com/delivery-delivery-external-service/sellers/{_apiSecret}/common-label/{cargoTrackingNumber}";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    return result;
  }

  #endregion
}