using System.Collections.Generic;
using System.Threading.Tasks;
using TrendyolSharp.Extensions;
using TrendyolSharp.Models;
using TrendyolSharp.Models.Base;
using TrendyolSharp.Models.Filter;
using TrendyolSharp.Models.Request;
using TrendyolSharp.Models.Response;
using TrendyolSharp.Utils;

namespace TrendyolSharp.Services.Marketplace
{
  public partial class TrendyolMarketplaceClient
  {
    #region QUESTIONS AND ANSWERS

    /// <summary>
    ///   https://developers.trendyol.com/en/docs/trendyol-marketplace/question-answer-integration/getting-customer-questions
    ///   <br /><br />
    ///   You can take all the questions asked by customers on Trendyol through this service.
    ///   <br /><br />
    ///   If you make a request with the above endpoint without giving any date parameters, your questions in the last week
    ///   will be shown to you. If you add the startDate and endDate parameters, the maximum interval will be two weeks.
    ///   <br /><br />
    ///   Recommended usage is with filters
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<TrendyolApiResult<ResponseGetQuestions>> GetQuestionsAsync(FilterGetQuestions filter = null) {
      var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/questions/filter";
      url = url.BuildUrl(
                         new KeyValuePair<string, object>("barcode", filter?.Barcode),
                         new KeyValuePair<string, object>("page", filter?.Page),
                         new KeyValuePair<string, object>("size", filter?.Size),
                         new KeyValuePair<string, object>("supplierId", filter?.SupplierId),
                         new KeyValuePair<string, object>("startDate", filter?.StartDate),
                         new KeyValuePair<string, object>("endDate", filter?.EndDate),
                         new KeyValuePair<string, object>("status", filter?.Status),
                         new KeyValuePair<string, object>("orderByField", filter?.OrderByField),
                         new KeyValuePair<string, object>("orderByDirection", filter?.OrderByDirection)
                        );
      var request = new TrendyolRequest(_httpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.ToObject<ResponseGetQuestions>();
      return result.WithData(data);
    }


    /// <summary>
    ///   https://developers.trendyol.com/en/docs/trendyol-marketplace/question-answer-integration/getting-customer-questions
    ///   <br /><br />
    ///   With the id value of the question returned from the service above, you can pull the questions individually and take
    ///   action.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TrendyolApiResult<Question>> GetQuestionsByIdAsync(int id) {
      //TODO: CHECK IF RESPONSE IS Question or ResponseGetQuestions assumed it is Question at the moment
      var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/questions/{id}";
      var request = new TrendyolRequest(_httpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.ToObject<Question>();
      return result.WithData(data);
    }

    /// <summary>
    ///   https://developers.trendyol.com/en/docs/trendyol-marketplace/question-answer-integration/answering-customer-questions
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    public async Task<TrendyolApiResult> CreateAnswerAsync(RequestCreateAnswer requestModel) {
      var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/questions/{requestModel.QuestionId}/answers";
      var request = new TrendyolRequest(_httpClient, url);
      var result = await request.SendPostRequestAsync(new {
        text = requestModel.Text
      });
      return result;
    }

    #endregion
  }
}