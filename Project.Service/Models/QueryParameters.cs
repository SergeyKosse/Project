using System;

namespace Project.Service.Models;

public class QueryParameters
{
  public int Page { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string? OrderBy { get; set; } = "Name";
  public bool Descending { get; set; } = false;
  public string? Filter { get; set; } = "";
  public Dictionary<string, string>? Filters { get; set; } = new Dictionary<string, string>();

public QueryParameters(){}
  public QueryParameters(QueryParameters other)
  {
    Page = other.Page;
    PageSize = other.PageSize;
    OrderBy = other.OrderBy;
    Descending = other.Descending;
    Filter = other.Filter;
  }
}