﻿namespace TrendyolSharp.Marketplace.Models.Base;

public class CategoryAttributeValue
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }
}