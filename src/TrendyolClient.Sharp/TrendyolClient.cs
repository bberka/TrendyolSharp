using System;
using System.Net.Http;
using System.Text;
using Serilog;
using TrendyolClient.Sharp.Models;
using TrendyolClient.Sharp.Services.Marketplace;

// using TrendyolClient.Sharp.Services.Finance;

namespace TrendyolClient.Sharp
{
  public class TrendyolClient
  {
    private readonly ILogger _logger;
    private string _apiKey;
    private string _apiSecret;
    private long _sellerId;

    /// <summary>
    ///   Constructor for Trendyol Marketplace Client
    /// </summary>
    /// <param name="sellerId">Trendyol seller API id</param>
    /// <param name="apiKey">Trendyol seller API key</param>
    /// <param name="apiSecret">Trendyol seller API secret</param>
    /// <param name="integrationCompanyNameForHeader">Set a company name for User-Agent header</param>
    /// <param name="isUseStageApi">Whether to use the stage api or not</param>
    /// <param name="logger">Set logger for trendyol marketplace api client, uses serilog</param>
    public TrendyolClient(long sellerId,
                          string apiKey,
                          string apiSecret,
                          string integrationCompanyNameForHeader = "SelfIntegration",
                          bool isUseStageApi = false,
                          ILogger logger = null) {
      SellerId = sellerId;
      ApiKey = apiKey;
      ApiSecret = apiSecret;
      IsUseStageApi = isUseStageApi;
      _logger = logger;
      var httpClient = new HttpClient();
      if (SellerId < 1) throw new ArgumentNullException(nameof(sellerId));

      httpClient.DefaultRequestHeaders.Add("User-Agent", $"{sellerId} - {integrationCompanyNameForHeader}");
      httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"))}");

      MarketPlace = new TrendyolMarketplaceClient(sellerId, httpClient, isUseStageApi, logger);
      // Finance = new TrendyolFinanceClient(sellerId, httpClient, isUseStageApi, logger);
    }

    public TrendyolMarketplaceClient MarketPlace { get; }
    // public TrendyolFinanceClient Finance { get; }

    public bool IsUseStageApi { get; set; }

    public string ApiSecret {
      get => _apiSecret;
      set {
        if (value == null) throw new ArgumentNullException(nameof(ApiSecret));
        if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(ApiSecret));

        _apiSecret = value;
      }
    }

    public string ApiKey {
      get => _apiKey;
      set {
        if (value == null) throw new ArgumentNullException(nameof(ApiKey));
        if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(ApiKey));

        _apiKey = value;
      }
    }

    public long SellerId {
      get => _sellerId;
      set {
        if (value < 1) throw new ArgumentException(nameof(SellerId));

        _sellerId = value;
      }
    }


    /// <summary>
    ///   Gracefully verifies the trendyol marketplace client credentials
    ///   <br />
    ///   <br />
    ///   It will send a GetProductsAsync request only asking for 1 item to verify the credentials
    /// </summary>
    /// <returns></returns>
    public bool VerifyCredentials(out TrendyolApiResult? result) {
      result = null;
      try {
        var isCredentialsValid = !string.IsNullOrEmpty(_apiKey) && !string.IsNullOrEmpty(_apiSecret);
        if (!isCredentialsValid) {
          _logger?.Error("Trendyol marketplace client credentials are not valid for supplier id: {SupplierId}", _sellerId);
          return false;
        }

        var trendyolApiResult = MarketPlace.GetSupplierAddressesAsync()
                                           .GetAwaiter()
                                           .GetResult();

        result = trendyolApiResult.ToApiResult();
        _logger?.Information("Trendyol marketplace client credentials are valid for supplier id: {SupplierId}", _sellerId);
        return result.Value.IsSuccessStatusCode;
      }
      catch (Exception ex) {
        _logger?.Error(ex, "Trendyol marketplace client credentials are not valid for supplier id: {SupplierId}", _sellerId);
        return false;
      }
    }
  }
}