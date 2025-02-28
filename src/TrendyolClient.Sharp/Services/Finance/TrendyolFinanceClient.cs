// using System.Net.Http;
// using Serilog;
//
// namespace TrendyolClient.Sharp.Services.Finance
// {
//   /// <summary>
//   ///   https://developers.trendyol.com/docs/category/10-trendyol-i%CC%87hracat-merkezi-entegrasyonu
//   /// </summary>
//   public partial class TrendyolFinanceClient
//   {
//     private readonly HttpClient HttpClient;
//     private readonly bool _isUseStageApi;
//     private readonly ILogger _logger;
//     private readonly long _supplierId;
//
//     /// <summary>
//     ///   Constructor for Trendyol Finance Client
//     /// </summary>
//     /// <param name="supplierId">Trendyol seller API id</param>
//     /// <param name="httpClient"></param>
//     /// <param name="isUseStageApi">Whether to use the stage api or not</param>
//     /// <param name="logger">Set logger for trendyol marketplace api client, uses serilog</param>
//     internal TrendyolFinanceClient(long supplierId,
//                                    HttpClient httpClient,
//                                    bool isUseStageApi = false,
//                                    ILogger logger = null) {
//       _supplierId = supplierId;
//       HttpClient = httpClient;
//       _isUseStageApi = isUseStageApi;
//       _logger = logger;
//     }
//   }
// }

