using System.Text;
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
  public TrendyolMarketplaceClient(string supplierId,
                                   string apiKey,
                                   string apiSecret,
                                   string token,
                                   string integrationCompanyNameForHeader = "SelfIntegration",
                                   bool isUseStageApi = false) {
    TrendyolLogger.Configure();
    _supplierId = supplierId;
    _apiKey = apiKey;
    _apiSecret = apiSecret;
    _token = token;
    var baseApiUrl = isUseStageApi
                       ? "https://stageapi.trendyol.com/sapigw"
                       : "https://api.trendyol.com/sapigw";

    _httpClient = new HttpClient();
    _httpClient.BaseAddress = new Uri(baseApiUrl);
    _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{supplierId} - {integrationCompanyNameForHeader}");
    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"))}");
  }


  public async Task<ResponseGetSuppliersAddresses> GetSupplierAddressesAsync() {
    var url = $"/suppliers/{_supplierId}/addresses";
    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    return result.Content.ToObject<ResponseGetSuppliersAddresses>();
  }

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

  public async Task<ResponseBatchRequestId> CreateProductsAsync(RequestCreateProducts request) {
    var url = $"/suppliers/{_supplierId}/v2/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    return result.Content.ToObject<ResponseBatchRequestId>();
  }

  public async Task<ResponseBatchRequestId> UpdateProductsAsync(RequestUpdateProduct request) {
    var url = $"/suppliers/{_supplierId}/v2/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    return result.Content.ToObject<ResponseBatchRequestId>();
  }

  public async Task<ResponseBatchRequestId> UpdatePriceAndInventoryAsync(RequestUpdatePriceAndInventory request) {
    var url = $"/suppliers/{_supplierId}/products/price-and-inventory";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    return result.Content.ToObject<ResponseBatchRequestId>();
  }

  public async Task<ResponseBatchRequestId> DeleteProductAsync(RequestDeleteProducts request) {
    var url = $"/suppliers/{_supplierId}/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendDeleteRequestAsync();
    return result.Content.ToObject<ResponseBatchRequestId>();
  }

  public async Task<ResponseGetBatchRequestResult> GetBatchRequestResultAsync(string batchRequestId) {
    var url = $"/suppliers/{_supplierId}/products/batch-requests/{batchRequestId}";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    return result.Content.ToObject<ResponseGetBatchRequestResult>();
  }
}