using System;
using System.Threading.Tasks;
using TrendyolClient.Sharp.Models;
using TrendyolClient.Sharp.Models.Marketplace.Request;

namespace TrendyolClient.Sharp.Services.Marketplace
{
  public partial class TrendyolMarketplaceClient
  {
    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/ortak-etiket-entegrasyonu/barkod-talebi
    /// </summary>
    public async Task<TrendyolApiResult> CreateCommonLabelAsync(string cargoTrackingNumber, RequestCreateCommonLabel request) {
      if (string.IsNullOrEmpty(cargoTrackingNumber)) throw new ArgumentException(nameof(cargoTrackingNumber));

      var prod = $"https://apigw.trendyol.com/integration/sellers/{SellerId}/common-label/{cargoTrackingNumber}";
      var stage = $"https://stageapigw.trendyol.com/integration/sellers/{SellerId}/common-label/{cargoTrackingNumber}";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendPostRequestAsync(request);
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/category/ortak-etiket-entegrasyonu
    /// </summary>
    public async Task<TrendyolApiResult> GetCommonLabelAsync(string cargoTrackingNumber) {
      if (string.IsNullOrEmpty(cargoTrackingNumber)) throw new ArgumentException(nameof(cargoTrackingNumber));

      var prod = $"https://apigw.trendyol.com/integration/sellers/{SellerId}/common-label/{cargoTrackingNumber}";
      var stage = $"https://stageapigw.trendyol.com/integration/sellers/{SellerId}/common-label/{cargoTrackingNumber}";
      var url = UseStageApi
                  ? stage
                  : prod;
      var trendyolRequest = new TrendyolRequest(HttpClient, url);
      var result = await trendyolRequest.SendGetRequestAsync();
      return result;
    }
  }
}