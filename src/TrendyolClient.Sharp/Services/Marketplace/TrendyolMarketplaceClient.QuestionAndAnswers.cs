using System.Collections.Generic;
using System.Threading.Tasks;
using TrendyolClient.Sharp.Extensions;
using TrendyolClient.Sharp.Models;
using TrendyolClient.Sharp.Models.Marketplace;
using TrendyolClient.Sharp.Models.Marketplace.Filter;
using TrendyolClient.Sharp.Models.Marketplace.Request;
using TrendyolClient.Sharp.Models.Marketplace.Response;
using TrendyolClient.Sharp.Utils;

namespace TrendyolClient.Sharp.Services.Marketplace
{
  public partial class TrendyolMarketplaceClient
  {
    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/soru-cevap-entegrasyonu/musteri-sorularini-cekme
    /// </summary>
    public async Task<TrendyolApiResult<ResponseGetQuestions>> GetQuestionsAsync(FilterGetQuestions filter = null) {
      var prod = $"https://apigw.trendyol.com/integration/qna/sellers/{SellerId}/questions/filter";
      var stage = $"https://stageapigw.trendyol.com/integration/qna/sellers/{SellerId}/questions/filter";
      var url = UseStageApi
                  ? stage
                  : prod;
      url = url.BuildUrl(
                         new Dictionary<string, object> {
                           { "barcode", filter?.Barcode },
                           { "page", filter?.Page },
                           { "size", filter?.Size },
                           { "supplierId", filter?.SupplierId },
                           { "startDate", filter?.StartDate },
                           { "endDate", filter?.EndDate },
                           { "status", filter?.Status },
                           { "orderByField", filter?.OrderByField },
                           { "orderByDirection", filter?.OrderByDirection }
                         }
                        );
      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.JsonToObject<ResponseGetQuestions>();
      return result.WithData(data);
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/soru-cevap-entegrasyonu/musteri-sorularini-cekme
    /// </summary>
    public async Task<TrendyolApiResult<Question>> GetQuestionsByIdAsync(int id) {
      //TODO: CHECK IF RESPONSE IS Question or ResponseGetQuestions assumed it is Question at the moment
      var prod = $"https://apigw.trendyol.com/integration/qna/sellers/{SellerId}/questions/{id}";
      var stage = $"https://stageapigw.trendyol.com/integration/qna/sellers/{SellerId}/questions/{id}";
      var url = UseStageApi
                  ? stage
                  : prod;
      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.JsonToObject<Question>();
      return result.WithData(data);
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/soru-cevap-entegrasyonu/musteri-sorularini-cevaplama
    /// </summary>
    public async Task<TrendyolApiResult> CreateAnswerAsync(long id, RequestCreateAnswer req) {
      var prod = $"https://apigw.trendyol.com/integration/qna/sellers/{SellerId}/questions/{id}/answers";
      var stage = $"https://stageapigw.trendyol.com/integration/qna/sellers/{SellerId}/questions/{id}/answers";
      var url = UseStageApi
                  ? stage
                  : prod;
      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendPostRequestAsync(req);
      return result;
    }
  }
}