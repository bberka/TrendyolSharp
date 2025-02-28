using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendyolClient.Sharp.Models;
using TrendyolClient.Sharp.Models.Marketplace.Filter;
using TrendyolClient.Sharp.Models.Marketplace.Request;
using TrendyolClient.Sharp.Models.Marketplace.Response;
using TrendyolClient.Sharp.Extensions;
using TrendyolClient.Sharp.Utils;

namespace TrendyolClient.Sharp.Services.Marketplace
{
  public partial class TrendyolMarketplaceClient
  {
    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/iade-ve-sevkiyat-adres-bilgileri
    /// </summary>
    public async Task<TrendyolApiResult<ResponseGetSuppliersAddresses>> GetSupplierAddressesAsync() {
      var prod = $"https://apigw.trendyol.com/integration/sellers/{SellerId}/addresses";
      var stage = $"https://stageapigw.trendyol.com/integration/sellers/{SellerId}/addresses";
      var url = UseStageApi
                  ? stage
                  : prod;
      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.JsonToObject<ResponseGetSuppliersAddresses>();
      return result.WithData(data);
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-marka-listesi
    /// </summary>
    public async Task<TrendyolApiResult<ResponseGetBrands>> GetBrandsAsync(FilterPage pageRequest = null) {
      var prod = $"https://apigw.trendyol.com/integration/product/brands";
      var stage = $"https://stageapigw.trendyol.com/integration/product/brands";
      var url = UseStageApi
                  ? stage
                  : prod;
      url = url.BuildUrl(
                         new Dictionary<string, object>() {
                           { "page", pageRequest?.Page },
                           { "size", pageRequest?.Size }
                         }
                        );

      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendGetRequestAsync();
      var data = result.Content.JsonToObject<ResponseGetBrands>();
      return result.WithData(data);
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-marka-listesi
    /// </summary>
    public async Task<TrendyolApiResult<ResponseGetBrandsByName>> GetBrandsByNameAsync(string brandName) {
      if (string.IsNullOrEmpty(brandName)) {
        throw new ArgumentNullException(nameof(brandName));
      }

      var prod = $"https://apigw.trendyol.com/integration/product/brands/by-name?name={brandName}";
      var stage = $"https://stageapigw.trendyol.com/integration/product/brands/by-name?name={brandName}";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendGetRequestAsync();
      var data = result.Content.JsonToObject<ResponseGetBrandsByName>();
      return result.WithData(data);
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-kategori-listesi
    /// </summary>
    public async Task<TrendyolApiResult<ResponseGetCategoryTree>> GetCategoryTreeAsync(FilterGetCategoryTree filter = null) {
      var prod = $"https://apigw.trendyol.com/integration/product/product-categories";
      var stage = $"https://stageapigw.trendyol.com/integration/product/product-categories";
      var url = UseStageApi
                  ? stage
                  : prod;
      url = url.BuildUrl(
                         new Dictionary<string, object>() {
                           { "id", filter?.Id },
                           { "parentId", filter?.ParentId },
                           { "name", filter?.Name },
                           { "subCategories", filter?.SubCategories }
                         }
                        );
      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.JsonToObject<ResponseGetCategoryTree>();
      return result.WithData(data);
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-kategori-ozellik-listesi
    /// </summary>
    public async Task<TrendyolApiResult<ResponseGetCategoryAttributes>> GetCategoryAttributesAsync(int categoryId, FilterGetCategoryAttributes filter = null) {
      var prod = $"https://apigw.trendyol.com/integration/product/product-categories/{categoryId}/attributes";
      var stage = $"https://stageapigw.trendyol.com/integration/product/product-categories/{categoryId}/attributes";
      var url = UseStageApi
                  ? stage
                  : prod;
      url = url.BuildUrl(
                         new Dictionary<string, object>() {
                           { "name", filter?.Name },
                           { "displayName", filter?.DisplayName },
                           { "attributeId", filter?.AttributeId },
                           { "attributeName", filter?.AttributeName },
                           { "allowCustom", filter?.AllowCustom },
                           { "required", filter?.Required },
                           { "slicer", filter?.Slicer },
                           { "varianter", filter?.Varianter },
                           { "attributeValueId", filter?.AttributeValueId },
                           { "attributeValueName", filter?.AttributeValueName }
                         }
                        );
      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.JsonToObject<ResponseGetCategoryAttributes>();
      return result.WithData(data);
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-aktarma-v2
    /// </summary>
    public async Task<TrendyolApiResult<ResponseBatchRequestId>> CreateProductsAsync(RequestCreateProducts request) {
      if (request is null) {
        throw new ArgumentNullException(nameof(request));
      }

      var prod = $"https://apigw.trendyol.com/integration/product/sellers/{SellerId}/products";
      var stage = $"https://stageapigw.trendyol.com/integration/product/sellers/{SellerId}/products";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPostRequestAsync(request);
      var data = result.Content.JsonToObject<ResponseBatchRequestId>();
      return result.WithData(data);
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-urun-bilgisi-guncelleme
    /// </summary>
    public async Task<TrendyolApiResult<ResponseBatchRequestId>> UpdateProductsAsync(RequestUpdateProduct request) {
      if (request is null) {
        throw new ArgumentNullException(nameof(request));
      }

      var prod = $"https://apigw.trendyol.com/integration/product/sellers/{SellerId}/products";
      var stage = $"https://stageapigw.trendyol.com/integration/product/sellers/{SellerId}/products";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPutRequestAsync(request);
      var data = result.Content.JsonToObject<ResponseBatchRequestId>();
      return result.WithData(data);
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/stok-ve-fiyat-guncelleme
    /// </summary>
    public async Task<TrendyolApiResult<ResponseBatchRequestId>> UpdatePriceAndInventoryAsync(RequestUpdatePriceAndInventory request) {
      if (request is null) {
        throw new ArgumentNullException(nameof(request));
      }

      var prod = $"https://apigw.trendyol.com/integration/inventory/sellers/{SellerId}/products/price-and-inventory";
      var stage = $"https://stageapigw.trendyol.com/integration/inventory/sellers/{SellerId}/products/price-and-inventory";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPostRequestAsync(request);
      var data = result.Content.JsonToObject<ResponseBatchRequestId>();
      return result.WithData(data);
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-silme
    /// </summary>
    public async Task<TrendyolApiResult<ResponseBatchRequestId>> DeleteProductAsync(RequestDeleteProducts request) {
      if (request is null) {
        throw new ArgumentNullException(nameof(request));
      }

      var prod = $"https://apigw.trendyol.com/integration/product/sellers/{SellerId}/products";
      var stage = $"https://stageapigw.trendyol.com/integration/product/sellers/{SellerId}/products";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendDeleteRequestAsync(request);
      var data = result.Content.JsonToObject<ResponseBatchRequestId>();
      return result.WithData(data);
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/toplu-islem-kontrolu
    ///  </summary>
    public async Task<TrendyolApiResult<ResponseGetBatchRequestResult>> GetBatchRequestResultAsync(string batchRequestId) {
      if (string.IsNullOrEmpty(batchRequestId)) {
        throw new ArgumentNullException(nameof(batchRequestId));
      }

      var prod = $"https://apigw.trendyol.com/integration/product/sellers/{SellerId}/products/batch-requests/{batchRequestId}";
      var stage = $"https://stageapigw.trendyol.com/integration/product/sellers/{SellerId}/products/batch-requests/{batchRequestId}";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendGetRequestAsync();
      var data = result.Content.JsonToObject<ResponseGetBatchRequestResult>();
      return result.WithData(data);
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-filtreleme
    /// </summary>
    public async Task<TrendyolApiResult<ResponseGetProductsFiltered>> GetProductsAsync(FilterProducts filter = null) {
      var prod = $"https://apigw.trendyol.com/integration/product/sellers/{SellerId}/products";
      var stage = $"https://stageapigw.trendyol.com/integration/product/sellers/{SellerId}/products";
      var url = UseStageApi
                  ? stage
                  : prod;
      url = url.BuildUrl(
                         new Dictionary<string, object> {
                           { "approved", filter?.Approved },
                           { "barcode", filter?.Barcode },
                           { "startDate", filter?.StartDate },
                           { "endDate", filter?.EndDate },
                           { "page", filter?.Page },
                           { "dateQueryType", filter?.DateQueryType },
                           { "size", filter?.Size },
                           { "supplierId", filter?.SupplierId },
                           { "stockCode", filter?.StockCode },
                           { "archived", filter?.Archived },
                           { "productMainId", filter?.ProductMainId },
                           { "onSale", filter?.OnSale },
                           { "rejected", filter?.Rejected },
                           { "blacklisted", filter?.Blacklisted },
                           { "brandIds", filter?.BrandIds }
                         }
                        );

      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.JsonToObject<ResponseGetProductsFiltered>();
      return result.WithData(data);
    }
  }
}