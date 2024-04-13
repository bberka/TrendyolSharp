using System.Net.Http;
using Serilog;

namespace TrendyolSharp.Services.Marketplace
{
  /// <summary>
  ///   https://developers.trendyol.com/docs/category/7-trendyol-marketplace-entegrasyonu
  /// </summary>
  public partial class TrendyolMarketplaceClient
  {
    private readonly HttpClient _httpClient;
    private readonly bool _isUseStageApi;
    private readonly ILogger _logger;
    private readonly long _supplierId;

    /// <summary>
    ///   Constructor for Trendyol Marketplace Client
    /// </summary>
    /// <param name="supplierId">Trendyol seller API id</param>
    /// <param name="httpClient"></param>
    /// <param name="isUseStageApi">Whether to use the stage api or not</param>
    /// <param name="logger">Set logger for trendyol marketplace api client, uses serilog</param>
    internal TrendyolMarketplaceClient(long supplierId,
                                       HttpClient httpClient,
                                       bool isUseStageApi = false,
                                       ILogger logger = null) {
      _supplierId = supplierId;
      _httpClient = httpClient;
      _isUseStageApi = isUseStageApi;
      _logger = logger;
    }
  }
}