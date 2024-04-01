﻿namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class SupplierAddress
{
  public int Id { get; set; }

  [Required]
  public string AddressType { get; set; }

  [Required]
  public string Country { get; set; }

  [Required]
  public string City { get; set; }

  public int CityCode { get; set; }

  [Required]
  public string District { get; set; }

  public int DistrictId { get; set; }

  [Required]
  public string PostCode { get; set; }

  [Required]
  public string Address { get; set; }

  public bool ReturningAddress { get; set; }

  [Required]
  public string FullAddress { get; set; }

  public bool ShipmentAddress { get; set; }

  public bool InvoiceAddress { get; set; }

  public bool Default { get; set; }
}