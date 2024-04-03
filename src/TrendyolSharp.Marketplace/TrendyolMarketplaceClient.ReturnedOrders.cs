using TrendyolSharp.Marketplace.Models.Request;
using TrendyolSharp.Marketplace.Models.Response;
using TrendyolSharp.Shared;
using TrendyolSharp.Shared.Extensions;
using TrendyolSharp.Shared.Models;

namespace TrendyolSharp.Marketplace;

public partial class TrendyolMarketplaceClient
{
  #region RETURNED ORDERS INTEGRATION

  /// <summary>
  /// https://developers.trendyol.com/en/docs/trendyol-marketplace/returned-orders-integration/getting-returned-orders
  /// <br/><br/>
  /// This method enables you to retrieve information about returned orders from the Trendyol system.
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetClaims>> GetClaims(RequestGetClaims? requestModel = null) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/claims";
    if (requestModel is not null) {
      if (!string.IsNullOrEmpty(requestModel.ClaimIds)) {
        url += $"?claimIds={requestModel.ClaimIds}";
      }

      if (!string.IsNullOrEmpty(requestModel.ClaimItemStatus)) {
        url += $"?claimItemStatus={requestModel.ClaimItemStatus}";
      }

      if (requestModel.EndDate.HasValue) {
        url += $"?endDate={requestModel.EndDate}";
      }

      if (requestModel.StartDate.HasValue) {
        url += $"?startDate={requestModel.StartDate}";
      }

      if (!string.IsNullOrEmpty(requestModel.OrderNumber)) {
        url += $"?orderNumber={requestModel.OrderNumber}";
      }

      if (requestModel.Size.HasValue) {
        url += $"?size={requestModel.Size}";
      }

      if (requestModel.Page.HasValue) {
        url += $"?page={requestModel.Page}";
      }
    }

    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetClaims>();
    return result.WithData(data);
  }

/// <summary>
/// https://developers.trendyol.com/en/docs/trendyol-marketplace/returned-orders-integration/create-a-return-request
/// <br/><br/>
/// You can use it to create return request packages for order packages that arrive without a return code. After creating a package with this service, you can get the return packages with Getting Returned Orders
///  <br/><br/>
/// The refund request you will create will be created in the status of "Created". You can use the "createClaim" service only for return requests you will "approve."
/// </summary>
/// <param name="requestModel"></param>
/// <returns></returns>
  public async Task<TrendyolApiResult<ResponseCreateClaim>> CreateClaimAsync(RequestCreateClaim requestModel) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/claims/create";
    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendPostRequestAsync(requestModel);
    var data = result.Content.ToObject<ResponseCreateClaim>();
    return result.WithData(data);
  }

  #endregion
}