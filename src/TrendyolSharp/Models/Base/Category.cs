using System.Collections.Generic;

namespace TrendyolSharp.Models.Base
{
  public sealed class Category
  {
    public long Id { get; set; }


    public string Name { get; set; }

    public long ParentId { get; set; }

    public List<SubCategory> SubCategories { get; set; }
  }
}