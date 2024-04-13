﻿namespace TrendyolSharp.Models.Filter
{
  public sealed class FilterGetCategoryTree
  {
    public long? Id { get; set; }
    public long? ParentId { get; set; }
    public string Name { get; set; }
    public bool? SubCategories { get; set; }
  }
}