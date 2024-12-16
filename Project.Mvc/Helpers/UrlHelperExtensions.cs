using System;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Models;

namespace Project.Mvc.Helpers
{
  public static class UrlHelperExtensions
  {
    public static string BuildUrl(
      this IUrlHelper urlHelper,
      string baseUrl,
      int page,
      Dictionary<string, string> queryParams)
    {
      var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

      foreach (var param in queryParams)
      {
        queryString[param.Key] = param.Value;
      }

      queryString["page"] = page.ToString();

      return $"{baseUrl}?{queryString}";
    }

    public static string BuildUrl(
      this IUrlHelper urlHelper,
      string baseUrl,
      Dictionary<string, string> queryParams,
      Dictionary<string, string> updatedQueryParams)
    {
      var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

      foreach (var param in queryParams)
      {
        queryString[param.Key] = param.Value;
      }

      foreach (var param in updatedQueryParams)
      {
        queryString[param.Key] = param.Value;
      }

      return $"{baseUrl}?{queryString}";
    }

    public static Dictionary<string, string> ToDictionary(this QueryParameters queryParams)
    {
      return new Dictionary<string, string>
        {
            { "Page", queryParams.Page.ToString() },
            { "PageSize", queryParams.PageSize.ToString() },
            { "OrderBy", queryParams.OrderBy ?? string.Empty },
            { "Descending", queryParams.Descending.ToString().ToLower() },
            { "Filter", queryParams.Filter ?? string.Empty }
        };
    }
  }


}
