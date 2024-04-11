using TrendyolSharp.Marketplace.Models.Filter;
using TrendyolSharp.Marketplace.Models.Response;
using TrendyolSharp.Shared;
using TrendyolSharp.Shared.Extensions;
using TrendyolSharp.Shared.Models;
using TrendyolSharp.Shared.Utils;

namespace TrendyolSharp.Marketplace;

public partial class TrendyolMarketplaceClient
{
  #region FINANCE INTEGRATION

  /// <summary>
  /// https://developers.trendyol.com/docs/category/4-muhasebe-ve-finans-entegrasyonu
  /// <br/><br/>
  /// Trendyol sisteminde oluşan muhasebesel kayıtlarınızı bu servis aracılığı ile entegrasyon üzerinden çekebilirsiniz.
  ///  <br/><br/>
  /// Finansal kayıtlar sipariş teslim edildikten sonra oluşmaktadır.
  /// <br/><br/>
  /// transactionType girilmesi zorunludur. 1 istekte yalnızca 1 type girilebilir.
  /// <br/><br/>
  /// paymentOrderId siparişin ödemesi yapıldıktan sonra oluşmaktadır. İstisnalar hariç, her çarşamba, ilgili haftada vadesi gelen siparişler için ödeme emri oluşur.
  /// <br/><br/>
  /// paymentOrderId ile sipariş ve ödemelerinizi eşleştirebilirsiniz.
  ///<br/><br/>
  /// Başlangıç ve bitiş tarihi girilmesi zorunludur ve arasındaki süre 15 günden uzun olamaz.
  ///<br/><br/>
  /// store bilgileri Hızlı Market satıcıları tarafından kullanılmaktadır. Marketplace satıcıları için "null" olarak dönecektir.
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetSettlements>> GetSettlementsAsync(FilterGetSettlements filter) {
    var url = $"https://api.trendyol.com/integration/finance/che/sellers/{_supplierId}/settlements";

    url = url.BuildUrl(
                       new("startDate", filter.StartDate.ToTimestampMiliseconds()),
                       new("endDate", filter.EndDate.ToTimestampMiliseconds()),
                       new("transactionType", filter.TransactionType),
                       
                       new("page", filter.Page),
                       new("size", filter.Size)
                      );

    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetSettlements>();
    return result.WithData(data);
  }

  #endregion
}