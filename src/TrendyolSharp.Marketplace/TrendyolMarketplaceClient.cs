using System.Text;
using Serilog;
using TrendyolSharp.Marketplace.Models;
using TrendyolSharp.Marketplace.Models.Filter;
using TrendyolSharp.Marketplace.Models.Request;
using TrendyolSharp.Marketplace.Models.Response;
using TrendyolSharp.Shared;
using TrendyolSharp.Shared.Common;
using TrendyolSharp.Shared.Extensions;


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
                       ? "https://stageapi.trendyol.com/sapigw"
                       : "https://api.trendyol.com/sapigw";

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
    var url = $"/suppliers/{_supplierId}/addresses";
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
    var url = "/brands";
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
    var url = $"/brands/by-name&name={name}";
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
    var url = "/product-categories";
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
    var url = $"/product-categories/{categoryId}/attributes";
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
    var url = $"/suppliers/{_supplierId}/v2/products";
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
    var url = $"/suppliers/{_supplierId}/v2/products";
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
    var url = $"/suppliers/{_supplierId}/products/price-and-inventory";
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
    var url = $"/suppliers/{_supplierId}/products";
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
    var url = $"/suppliers/{_supplierId}/products/batch-requests/{batchRequestId}";
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
    var url = $"/suppliers/{_supplierId}/products";
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
}