using System.Collections.Generic;
using System.Threading.Tasks;
using TrendyolClient.Sharp.Models;
using TrendyolClient.Sharp.Models.Marketplace.Request;
using TrendyolClient.Sharp.Models.Marketplace.Response;
using TrendyolClient.Sharp.Extensions;
using TrendyolClient.Sharp.Utils;

namespace TrendyolClient.Sharp.Services.Marketplace
{
  public partial class TrendyolMarketplaceClient
  {
    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/iade-entegrasyonu/iade-olusturulan-siparisleri-cekme
    /// </summary>
    public async Task<TrendyolApiResult<ResponseGetClaims>> GetClaimsAsync(RequestGetClaims req = null) {
      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/claims";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/claims";
      var url = UseStageApi
                  ? stage
                  : prod;

      url = url.BuildUrl(
                         new Dictionary<string, object> {
                           { "claimIds", req?.ClaimIds },
                           { "claimItemStatus", req?.ClaimItemStatus },
                           { "endDate", req?.EndDate },
                           { "startDate", req?.StartDate },
                           { "orderNumber", req?.OrderNumber },
                           { "size", req?.Size },
                           { "page", req?.Page }
                         }
                        );

      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.JsonToObject<ResponseGetClaims>();
      return result.WithData(data);
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/iade-entegrasyonu/iade-siparisleri-onaylama
    /// </summary>
    public async Task<TrendyolApiResult> ApproveClaimLineItemsAsync(string claimId, RequestApproveClaimLineItems req) {
      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/claims/{claimId}/items/approve";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/claims/{claimId}/items/approve";
      var url = UseStageApi
                  ? stage
                  : prod;

      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendPutRequestAsync(req);
      return result;
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/iade-entegrasyonu/iade-siparislerinde-red-talebi-olusturma
    /// </summary>
    public async Task<TrendyolApiResult<ResponseCreateClaim>> CreateClaimIssueAsync(RequestCreateClaimIssue req) {
      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/claims/{req.ClaimId}/issue?claimIssueReasonId={req.ClaimIssueReasonId}&claimItemIdList={req.ClaimItemIdList}&description={req.Description}";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/claims/{req.ClaimId}/issue?claimIssueReasonId={req.ClaimIssueReasonId}&claimItemIdList={req.ClaimItemIdList}&description={req.Description}";
      var url = UseStageApi
                  ? stage
                  : prod;
      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendPostRequestAsync();
      var data = result.Content.JsonToObject<ResponseCreateClaim>();
      return result.WithData(data);
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/iade-entegrasyonu/iade-red-sebeplerini-cekme
    /// </summary>
    public async Task<TrendyolApiResult<ResponseGetClaimsIssueReasons>> GetClaimsIssueReasonsAsync() {
      var prod = $"https://apigw.trendyol.com/integration/order/claim-issue-reasons";
      var stage = $"https://stageapigw.trendyol.com/integration/order/claim-issue-reasons";
      var url = UseStageApi
                  ? stage
                  : prod;
      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.JsonToObject<ResponseGetClaimsIssueReasons>();
      return result.WithData(data);
    }

    /// <summary>
    ///   https://developers.trendyol.com/docs/marketplace/iade-entegrasyonu/iade-audit-bilgilerini-cekme
    /// </summary>
    public async Task<TrendyolApiResult<ResponseGetClaimAuditInformation>> GetClaimAuditInformationAsync(string claimItemsId) {
      var prod = $"https://apigw.trendyol.com/integration/order/sellers/{SellerId}/claims/items/{claimItemsId}/audit";
      var stage = $"https://stageapigw.trendyol.com/integration/order/sellers/{SellerId}/claims/items/{claimItemsId}/audit";
      var url = UseStageApi
                  ? stage
                  : prod;
      var request = new TrendyolRequest(HttpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.JsonToObject<ResponseGetClaimAuditInformation>();
      return result.WithData(data);
    }
  }
}