using System.Runtime;
using System.Text;
using Serilog;
using TrendyolSharp.Marketplace.Models;
using TrendyolSharp.Marketplace.Models.Filter;
using TrendyolSharp.Marketplace.Models.Request;
using TrendyolSharp.Marketplace.Models.Response;
using TrendyolSharp.Shared;
using TrendyolSharp.Shared.Common;
using TrendyolSharp.Shared.Extensions;
using TrendyolSharp.Shared.Models;


namespace TrendyolSharp.Marketplace;

public partial class TrendyolMarketplaceClient
{
  private readonly long _supplierId;
  private readonly string _apiKey;
  private readonly string _apiSecret;
  private readonly string _token;
  private readonly bool _isUseStageApi;
  private readonly ILogger? _logger;

  public string ApiKey => _apiKey;
  public string ApiSecret => _apiSecret;
  public string Token => _token;
  public bool IsUseStageApi => _isUseStageApi;
  public ILogger? Logger => _logger;

  private readonly HttpClient _httpClient;

  /// <summary>
  ///  Constructor for Trendyol Marketplace Client
  /// </summary>
  /// <param name="supplierId">Trendyol seller API id</param>
  /// <param name="apiKey">Trendyol seller API key</param>
  /// <param name="apiSecret">Trendyol seller API secret</param>
  /// <param name="token">Trendyol seller API token</param>
  /// <param name="integrationCompanyNameForHeader">Set a company name for User-Agent header</param>
  /// <param name="isUseStageApi">Whether to use the stage api or not</param>
  /// <param name="logger">Set logger for trendyol marketplace api client, uses serilog</param>
  public TrendyolMarketplaceClient(long supplierId,
                                   string apiKey,
                                   string apiSecret,
                                   string token,
                                   string integrationCompanyNameForHeader = "SelfIntegration",
                                   bool isUseStageApi = false,
                                   ILogger? logger = null) {
    ArgumentNullException.ThrowIfNull(apiKey);
    ArgumentNullException.ThrowIfNull(apiSecret);
    ArgumentNullException.ThrowIfNull(token);
    ArgumentNullException.ThrowIfNull(integrationCompanyNameForHeader);
    ArgumentNullException.ThrowIfNull(isUseStageApi);
    _supplierId = supplierId;
    _apiKey = apiKey;
    _apiSecret = apiSecret;
    _token = token;
    _isUseStageApi = isUseStageApi;
    _logger = logger;
    _httpClient = new HttpClient();
    if (_supplierId < 1) {
      throw new ArgumentNullException(nameof(supplierId));
    }

    _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{supplierId} - {integrationCompanyNameForHeader}");
    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"))}");
  }

  
  /// <summary>
  /// Gracefully verifies the trendyol marketplace client credentials
  /// <br/>
  /// <br/>
  /// It will send a GetProductsAsync request only asking for 1 item to verify the credentials
  /// </summary>
  /// <returns></returns>
  public bool VerifyCredentials() {
    try {
      var isCredentialsValid = !string.IsNullOrEmpty(_apiKey) && !string.IsNullOrEmpty(_apiSecret) && !string.IsNullOrEmpty(_token);
      if (!isCredentialsValid) {
        _logger?.Error("Trendyol marketplace client credentials are not valid for supplier id: {SupplierId}", _supplierId);
        return false;
      }

      var verifyResult = GetSupplierAddressesAsync()
                         .GetAwaiter()
                         .GetResult();
      _logger?.Information("Trendyol marketplace client credentials are valid for supplier id: {SupplierId}", _supplierId);
      return verifyResult.IsSuccessStatusCode;
    }
    catch (Exception ex) {
      _logger?.Error(ex, "Trendyol marketplace client credentials are not valid for supplier id: {SupplierId}", _supplierId);
      return false;
    }
  }
}