using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
// using System.Collections.Generic;
using System.Text;

namespace Project.Mvc.Helpers;
public static class PaginationHelper
{
    public static IHtmlContent Pagination(
        this IHtmlHelper htmlHelper,
        int currentPage,
        int totalPages,
        string baseUrl,
        Dictionary<string, string>? queryParams = null)
    {
        var sb = new StringBuilder();
        sb.Append("<nav><ul class='pagination'>");

        // prev page
        if (currentPage > 1)
        {
            sb.Append($"<li class='page-item'><a class='page-link' href='{BuildUrl(baseUrl, currentPage - 1, queryParams)}'>Previous</a></li>");
        }
        else
        {
            sb.Append("<li class='page-item disabled'><span class='page-link'>Previous</span></li>");
        }

        // links to pages
        for (int i = 1; i <= totalPages; i++)
        {
            if (i == currentPage)
            {
                sb.Append($"<li class='page-item active'><span class='page-link'>{i}</span></li>");
            }
            else
            {
                sb.Append($"<li class='page-item'><a class='page-link' href='{BuildUrl(baseUrl, i, queryParams)}'>{i}</a></li>");
            }
        }

        // next page
        if (currentPage < totalPages)
        {
            sb.Append($"<li class='page-item'><a class='page-link' href='{BuildUrl(baseUrl, currentPage + 1, queryParams)}'>Next</a></li>");
        }
        else
        {
            sb.Append("<li class='page-item disabled'><span class='page-link'>Next</span></li>");
        }

        sb.Append("</ul></nav>");

        return new HtmlString(sb.ToString());
    }

    private static string BuildUrl(string baseUrl, int page, Dictionary<string, string>? queryParams)
    {
        var url = new StringBuilder(baseUrl);
        url.Append($"?page={page}");

        if (queryParams != null)
        {
            foreach (var param in queryParams)
            {
                url.Append($"&{param.Key}={param.Value}");
            }
        }

        return url.ToString();
    }
}
