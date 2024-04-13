using System.Threading.Tasks;
using TrendyolSharp.Extensions;
using TrendyolSharp.Models;
using TrendyolSharp.Models.Response;

namespace TrendyolSharp.Services.Finance
{
  public partial class TrendyolFinanceClient
  {
    #region CARGO DETAILS

    /// <summary>
    ///   https://developers.trendyol.com/docs/muhasebe-ve-finans-entegrasyonu/kargo-faturasi-entegrasyonu
    ///   <br /><br />
    ///   Cari Hesap Ekstresi Entegrasyonu üzerinden transactionType='DeductionInvoices' responsundan dönen data içerisinde ki
    ///   alanlardan transactionType değeri "Kargo Faturası" yada "Kargo Fatura" olan kayıtların "Id" değeri
    ///   "invoiceSerialNumber" değeridir.
    /// </summary>
    /// <param name="invoiceSerialNumber"></param>
    /// <returns></returns>
    public async Task<TrendyolApiResult<ResponseGetCargoInvoiceDetails>> GetCargoInvoiceDetails(string invoiceSerialNumber) {
      var url = $"https://api.trendyol.com/integration/finance/che/sellers/{_supplierId}/cargo-invoice/{invoiceSerialNumber}/items";
      var request = new TrendyolRequest(_httpClient, url);
      var result = await request.SendGetRequestAsync();
      var data = result.Content.ToObject<ResponseGetCargoInvoiceDetails>();
      return result.WithData(data);
    }

    #endregion
  }
}