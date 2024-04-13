using System.Threading.Tasks;
using TrendyolSharp.Models;
using TrendyolSharp.Models.Request;

namespace TrendyolSharp.Services.Marketplace
{
  public partial class TrendyolMarketplaceClient
  {
    #region COMMON LABEL INTEGRATION

    //DOC: https://developers.trendyol.com/en/docs/trendyol-marketplace/common-label-integration/common-label-integration

    /// <summary>
    ///   https://developers.trendyol.com/en/docs/trendyol-marketplace/common-label-integration/barcode-request
    ///   <br />
    ///   <br />
    ///   The common label process can be used for TEX, Aras Kargo and UPS by supplier paid cargo model.
    ///   <br /><br />
    ///   With this service, you can request a barcode for the relevant cargo number for orders in the common label process.
    ///   After the barcode is created, it will return to you from the service in ZPL format.
    ///   <br /><br />
    ///   It is recommended to make the barcode request after picking or feeding invoiced status to the relevant order package.
    ///   As a format, there is only the ZPL format available for now.
    ///   <br /><br />
    ///   200	Success - No response body
    ///   <br />
    ///   400	Error - Check the cargoTrackingNumber value you have forwarded
    ///   <br />
    ///   401	Error - You need to check the API information you entered
    /// </summary>
    /// <returns></returns>
    public async Task<TrendyolApiResult> CreateCommonLabelAsync(string cargoTrackingNumber, RequestCreateCommonLabel request) {
      var url = $"https://sellerpublic-sdc.trendyol.com/delivery-delivery-external-service/sellers/{_supplierId}/common-label/{cargoTrackingNumber}";
      var trendyolRequest = new TrendyolRequest(_httpClient, url);
      var result = await trendyolRequest.SendPostRequestAsync(request);
      return result;
    }

    /// <summary>
    ///   https://developers.trendyol.com/en/docs/trendyol-marketplace/common-label-integration/getting-created-common-label
    ///   <br /> <br />
    ///   The common label process can be used for TEX, Aras Kargo and UPS by supplier paid cargo model.
    ///   <br /> <br />
    ///   With this service, you can get the value in the format you created the barcode request.
    ///   <br /> <br />
    ///   Multiple labels will be returned to you for multiple parcels. In case of a single parcel, one ZPL will be returned.
    ///   <br /> <br />
    ///   400	Error - Check the cargoTrackingNumber value you have forwarded
    ///   <br />
    ///   401	Error - You need to check the API information you entered
    /// </summary>
    /// <param name="cargoTrackingNumber"></param>
    /// <returns></returns>
    public async Task<TrendyolApiResult> GetCommonLabelAsync(string cargoTrackingNumber) {
      var url = $"https://sellerpublic-sdc.trendyol.com/delivery-delivery-external-service/sellers/{_supplierId}/common-label/{cargoTrackingNumber}";
      var trendyolRequest = new TrendyolRequest(_httpClient, url);
      var result = await trendyolRequest.SendGetRequestAsync();
      return result;
    }

    #endregion
  }
}