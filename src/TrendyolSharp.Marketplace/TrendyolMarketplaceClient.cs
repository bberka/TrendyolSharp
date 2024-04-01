using TrendyolSharp.Marketplace.Request;
using TrendyolSharp.Shared;
using TrendyolSharp.Shared.Extensions;
using TrendyolSharp.Shared.Models.Marketplace;
using TrendyolSharp.Shared.Models.Marketplace.Request;
using TrendyolSharp.Shared.Models.Marketplace.Response;

namespace TrendyolSharp.Marketplace;

public class TrendyolMarketplaceClient
{
  public TrendyolMarketplaceClient() {
    _httpClient = new HttpClient();
    //TODO: SET HEADERS
  }

  private readonly HttpClient _httpClient;


  public async Task<SupplierAddressResponse> GetSupplierAddressesAsync(int supplierId) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{supplierId}/addresses";
    var request = new TrendyolRequest(_httpClient, url.Replace("{supplierId}", supplierId.ToString()));
    var result = await request.SendGetRequestAsync();
    return result.Content.ToObject<SupplierAddressResponse>();
  }

  public async Task<BrandResponse> GetBrandsAsync() {
    const string url = "https://api.trendyol.com/sapigw/brands";
    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    return result.Content.ToObject<BrandResponse>();
  }

  public async Task<CategoryResponse> GetCategoryTree() {
    const string url = "https://api.trendyol.com/sapigw/product-categories";
    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    return result.Content.ToObject<CategoryResponse>();
  }

  public async Task<CategoryAttributeResponse> GetCategoryAttributesAsync(int categoryId) {
    var url = $"https://api.trendyol.com/sapigw/product-categories/{categoryId}/attributes";
    var request = new TrendyolRequest(_httpClient, url.Replace("{categoriId}", categoryId.ToString()));
    var result = await request.SendGetRequestAsync();
    return result.Content.ToObject<CategoryAttributeResponse>();
  }

  public async Task<CreateProductsRequest> CreateProductsAsync(int supplierId, CreateProductsRequest request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{supplierId}/v2/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    return result.Content.ToObject<CreateProductsRequest>();
  }
}