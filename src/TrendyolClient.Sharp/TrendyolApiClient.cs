using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using TrendyolClient.Sharp.Models;
using TrendyolClient.Sharp.Services.Marketplace;

// using TrendyolClient.Sharp.Services.Finance;

namespace TrendyolClient.Sharp
{
  public class TrendyolApiClient
  {
    private readonly ILogger _logger;

    /// <summary>
    ///   Constructor for Trendyol Marketplace Client
    /// </summary>
    /// <param name="sellerId">Trendyol seller API id</param>
    /// <param name="apiKey">Trendyol seller API key</param>
    /// <param name="apiSecret">Trendyol seller API secret</param>
    /// <param name="integrationCompanyNameForHeader">Set a company name for User-Agent header</param>
    /// <param name="useStageApi">Whether to use the stage api or not</param>
    /// <param name="logger">Set logger for trendyol marketplace api client, uses serilog</param>
    public TrendyolApiClient(long sellerId,
                             string apiKey,
                             string apiSecret,
                             string integrationCompanyNameForHeader = "SelfIntegration",
                             bool useStageApi = false,
                             ILogger logger = null) {
      SellerId = sellerId;
      ApiKey = apiKey;
      ApiSecret = apiSecret;
      UseStageApi = useStageApi;
      _logger = logger;
      var httpClient = new HttpClient();
      if (SellerId < 1) throw new ArgumentNullException(nameof(sellerId));

      httpClient.DefaultRequestHeaders.Add("User-Agent", $"{sellerId} - {integrationCompanyNameForHeader}");
      httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"))}");

      MarketPlace = new TrendyolMarketplaceClient(sellerId, httpClient, useStageApi, logger);
      // Finance = new TrendyolFinanceClient(sellerId, httpClient, isUseStageApi, logger);
    }

    public TrendyolMarketplaceClient MarketPlace { get; }
    // public TrendyolFinanceClient Finance { get; }

    public bool UseStageApi { get; protected set; }
    public string ApiSecret { get; protected set; }
    public string ApiKey { get; protected set; }
    public long SellerId { get; protected set; }


    /// <summary>
    ///   Gracefully verifies the trendyol marketplace client credentials
    ///   <br />
    ///   <br />
    ///   It will send a GetProductsAsync request only asking for 1 item to verify the credentials
    ///   <br />
    /// Check logs for details
    /// </summary>
    /// <returns></returns>
    public async Task<bool> VerifyCredentialsAsync() {
      try {
        var isCredentialsValid = !string.IsNullOrEmpty(ApiKey) && !string.IsNullOrEmpty(ApiSecret);
        if (!isCredentialsValid) {
          _logger?.Error("Trendyol marketplace client credentials are not valid for supplier id: {SellerId}", SellerId);
          return false;
        }

        var trendyolApiResult = await MarketPlace.GetSupplierAddressesAsync();
        _logger?.Information("Trendyol marketplace client credentials are valid for supplier id: {SellerId}", SellerId);
        return trendyolApiResult.IsSuccessStatusCode;
      }
      catch (Exception ex) {
        _logger?.Error(ex, "Trendyol marketplace client credentials are not valid for supplier id: {SellerId}", SellerId);
        return false;
      }
    }
  }
}