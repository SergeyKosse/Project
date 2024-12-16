using System;
using Microsoft.Extensions.Options;

namespace Project.Service.Models;

public class PagedResult<T>
{
  public List<T> Items { get; set; } = new List<T>();
  public int TotalCount { get; set; }
  public int Page { get; set; }
  public int PageSize { get; set; }

  public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

  public PagedResult()
  {

  }
  public PagedResult(IOptions<Pagination> pagination)
  {
    PageSize = pagination.Value.PageSize;
  }
}