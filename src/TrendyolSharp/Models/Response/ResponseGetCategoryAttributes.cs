using System.Collections.Generic;
using TrendyolSharp.Models.Base;

namespace TrendyolSharp.Models.Response
{
  public sealed class ResponseGetCategoryAttributes
  {
    public long Id { get; set; }


    public string Name { get; set; }


    public string DisplayName { get; set; }

    public List<CategoryAttribute> CategoryAttributes { get; set; }
  }
}