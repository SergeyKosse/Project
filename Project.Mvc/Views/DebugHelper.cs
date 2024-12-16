using System;
using Microsoft.AspNetCore.Html;

namespace Project.Mvc.Views;

public static class DebugHelper
{
    public static HtmlString Inspect(this object obj)
    {
        if (obj == null) return new HtmlString("null");

        var properties = obj.GetType().GetProperties();
        var result = string.Join("\n", properties.Select(p =>
        {
            var value = p.GetValue(obj) ?? "null";
            return $"{p.Name}: {value}";
        }));

        return new HtmlString($"<pre>{result}</pre>");
    }
}