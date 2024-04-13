using System.Collections.Generic;
using System.Threading.Tasks;
using TrendyolSharp.Extensions;
using TrendyolSharp.Models;
using TrendyolSharp.Models.Filter;
using TrendyolSharp.Models.Response;
using TrendyolSharp.Utils;

namespace TrendyolSharp.Services.Finance
{
  public partial class TrendyolFinanceClient
  {
    #region FINANCE INTEGRATION

    /// <summary>
    ///   https://developers.trendyol.com/docs/category/4-muhasebe-ve-finans-entegrasyonu
    ///   <br /><br />
    ///   Trendyol sisteminde oluşan muhasebesel kayıtlarınızı bu servis aracılığı ile entegrasyon üzerinden çekebilirsiniz.
    ///   <br /><br />
    ///   Finansal kayıtlar sipariş teslim edildikten sonra oluşmaktadır.
    ///   <br /><br />
    ///   transactionType girilmesi zorunludur. 1 istekte yalnızca 1 type girilebilir.
    ///   <br /><br />
    ///   paymentOrderId siparişin ödemesi yapıldıktan sonra oluşmaktadır. İstisnalar hariç, her çarşamba, ilgili haftada
    ///   vadesi gelen siparişler için ödeme emri oluşur.
    ///   <br /><br />
    ///   paymentOrderId ile sipariş ve ödemelerinizi eşleştirebilirsiniz.
    ///   <br /><br />
    ///   Başlangıç ve bitiş tarihi girilmesi zorunludur ve arasındaki süre 15 günden uzun olamaz.
    ///   <br /><br />
    ///   store bilgileri Hızlı Market satıcıları tarafından kullanılmaktadır. Marketplace satıcıları için "null" olarak
    ///   dönecektir.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<TrendyolApiResult<ResponseGetSettlements>> GetSettlementsAsync(FilterGetSettlements filter) {
      var url = $"https://api.trendyol.com/integration/finance/che/sellers/{_supplierId}/settlements";

      url = url.BuildUrl(
                         new KeyValuePair<string, object>("startDate", filter.StartDate.ToTimestampMiliseconds()),
                         new KeyValuePair<string, object>("endDate", filter.EndDate.ToTimestampMiliseconds()),
                         new KeyValuePair<string, object>("transactionType", filter.TransactionType),
                         new KeyValuePair<string, object>("page", filter.Page),
                         new KeyValuePair<string, object>("size", filter.Size)
                        );

      var request = new TrendyolRequest(_httpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.ToObject<ResponseGetSettlements>();
      return result.WithData(data);
    }


    /// <summary>
    ///   https://developers.trendyol.com/docs/category/4-muhasebe-ve-finans-entegrasyonu#get-otherfinancials-tedarik%C3%A7i-finansman%C4%B1-virman-%C3%B6demeler-faturalar-tedarik%C3%A7i-faturalar%C4%B1-gelen-havale-komisyon-mutabakat-faturalar%C4%B1
    ///   <br /><br />
    ///   Tedarikçi finansmanı, virman, ödemeler, faturalar, tedarikçi faturaları, gelen havale, komisyon mutabakat faturaları
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<TrendyolApiResult<ResponseGetOtherFinancials>> GetOtherFinancials(FilterGetOtherFinancials filter) {
      var url = $"https://api.trendyol.com/integration/finance/che/sellers/{_supplierId}/otherfinancials";

      url = url.BuildUrl(
                         new KeyValuePair<string, object>("startDate", filter.StartDate.ToTimestampMiliseconds()),
                         new KeyValuePair<string, object>("endDate", filter.EndDate.ToTimestampMiliseconds()),
                         new KeyValuePair<string, object>("page", filter.Page),
                         new KeyValuePair<string, object>("size", filter.Size),
                         new KeyValuePair<string, object>("transactionType", filter.TransactionType)
                        );

      var request = new TrendyolRequest(_httpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.ToObject<ResponseGetOtherFinancials>();
      return result.WithData(data);
    }

    #endregion
  }
}