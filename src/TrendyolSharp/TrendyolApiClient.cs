using System;
using System.Net.Http;
using System.Text;
using Serilog;
using TrendyolSharp.Models;
using TrendyolSharp.Services.Finance;
using TrendyolSharp.Services.Marketplace;

namespace TrendyolSharp
{
  public class TrendyolApiClient
  {
    private readonly ILogger _logger;
    private string _apiKey;
    private string _apiSecret;
    private long _supplierId;
    private string _token;

    /// <summary>
    ///   Constructor for Trendyol Marketplace Client
    /// </summary>
    /// <param name="supplierId">Trendyol seller API id</param>
    /// <param name="apiKey">Trendyol seller API key</param>
    /// <param name="apiSecret">Trendyol seller API secret</param>
    /// <param name="token">Trendyol seller API token</param>
    /// <param name="integrationCompanyNameForHeader">Set a company name for User-Agent header</param>
    /// <param name="isUseStageApi">Whether to use the stage api or not</param>
    /// <param name="logger">Set logger for trendyol marketplace api client, uses serilog</param>
    public TrendyolApiClient(long supplierId,
                             string apiKey,
                             string apiSecret,
                             string token,
                             string integrationCompanyNameForHeader = "SelfIntegration",
                             bool isUseStageApi = false,
                             ILogger logger = null) {
      SupplierId = supplierId;
      ApiKey = apiKey;
      ApiSecret = apiSecret;
      Token = token;
      IsUseStageApi = isUseStageApi;
      _logger = logger;
      var httpClient = new HttpClient();
      if (SupplierId < 1) throw new ArgumentNullException(nameof(supplierId));

      httpClient.DefaultRequestHeaders.Add("User-Agent", $"{supplierId} - {integrationCompanyNameForHeader}");
      httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"))}");

      MarketPlace = new TrendyolMarketplaceClient(supplierId, httpClient, isUseStageApi, logger);
      Finance = new TrendyolFinanceClient(supplierId, httpClient, isUseStageApi, logger);
    }

    public TrendyolMarketplaceClient MarketPlace { get; }
    public TrendyolFinanceClient Finance { get; }

    public bool IsUseStageApi { get; set; }

    public string Token {
      get => _token;
      set {
        if (value == null) throw new ArgumentNullException(nameof(Token));
        if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Token));

        _token = value;
      }
    }

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

    public long SupplierId {
      get => _supplierId;
      set {
        if (value < 1) throw new ArgumentException(nameof(SupplierId));

        _supplierId = value;
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
        var isCredentialsValid = !string.IsNullOrEmpty(_apiKey) && !string.IsNullOrEmpty(_apiSecret) && !string.IsNullOrEmpty(_token);
        if (!isCredentialsValid) {
          _logger?.Error("Trendyol marketplace client credentials are not valid for supplier id: {SupplierId}", _supplierId);
          return false;
        }

        var trendyolApiResult = MarketPlace.GetSupplierAddressesAsync()
                                           .GetAwaiter()
                                           .GetResult();

        result = trendyolApiResult.ToApiResult();
        _logger?.Information("Trendyol marketplace client credentials are valid for supplier id: {SupplierId}", _supplierId);
        return result.Value.IsSuccessStatusCode;
      }
      catch (Exception ex) {
        _logger?.Error(ex, "Trendyol marketplace client credentials are not valid for supplier id: {SupplierId}", _supplierId);
        return false;
      }
    }
  }
}