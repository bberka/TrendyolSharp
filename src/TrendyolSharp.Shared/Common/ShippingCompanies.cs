namespace TrendyolSharp.Shared.Common;

/// <summary>
/// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-kargo-sirketleri-listesi
/// </summary>
/// <param name="Id"></param>
/// <param name="Code"></param>
/// <param name="Name"></param>
/// <param name="TaxNumber"></param>
public record ShippingCompanies(int Id, string Code, string Name, string TaxNumber)
{
  //LAST UPDATED: 2024-04-02
  public static readonly ShippingCompanies[] List = new[] {
    new ShippingCompanies(42, "DHLMP", "DHL Marketplace", "951-241-77-13"),
    new ShippingCompanies(38, "SENDEOMP", "Sendeo Marketplace", "2910804196"),
    new ShippingCompanies(35, "OCTOMP", "Octovan Lojistik Marketplace", "6330506845"),
    new ShippingCompanies(30, "BORMP", "Borusan Lojistik Marketplace", "1800038254"),
    new ShippingCompanies(14, "CAIMP", "Cainiao Marketplace", "0"),
    new ShippingCompanies(10, "MNGMP", "MNG Kargo Marketplace", "6080712084"),
    new ShippingCompanies(19, "PTTMP", "PTT Kargo Marketplace", "7320068060"),
    new ShippingCompanies(9, "SURATMP", "Sürat Kargo Marketplace", "7870233582"),
    new ShippingCompanies(17, "TEXMP", "Trendyol Express Marketplace", "8590921777"),
    new ShippingCompanies(6, "HOROZMP", "Horoz Kargo Marketplace", "4630097122"),
    new ShippingCompanies(20, "CEVAMP", "CEVA Marketplace", "8450298557"),
    new ShippingCompanies(4, "YKMP", "Yurtiçi Kargo Marketplace", "3130557669"),
    new ShippingCompanies(7, "ARASMP", "Aras Kargo Marketplace", "720039666")
  };
}