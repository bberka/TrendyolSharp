using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace.Response
{
  public sealed class ResponseGetCategoryAttributes
  {
    public long Id { get; set; }


    public string Name { get; set; }


    public string DisplayName { get; set; }

    public List<CategoryAttribute> CategoryAttributes { get; set; }
  }
}