using System.Net.Http;
using Serilog;

namespace TrendyolClient.Sharp.Services.Marketplace
{
  /// <summary>
  ///   https://developers.trendyol.com/docs/category/7-trendyol-marketplace-entegrasyonu
  /// </summary>
  public partial class TrendyolMarketplaceClient
  {
    /// <summary>
    ///   Constructor for Trendyol Marketplace Client
    /// </summary>
    /// <param name="sellerId">Trendyol seller API id</param>
    /// <param name="httpClient"></param>
    /// <param name="useStageApi">Whether to use the stage api or not</param>
    /// <param name="logger">Set logger for trendyol marketplace api client, uses serilog</param>
    protected internal TrendyolMarketplaceClient(long sellerId,
                                                 HttpClient httpClient,
                                                 bool useStageApi = false,
                                                 ILogger logger = null) {
      SellerId = sellerId;
      HttpClient = httpClient;
      UseStageApi = useStageApi;
      Logger = logger;
    }

    public long SellerId { get; }
    public bool UseStageApi { get; }
    protected HttpClient HttpClient { get; }
    protected ILogger Logger { get; }
  }
}