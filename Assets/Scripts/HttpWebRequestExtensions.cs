using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

public static class HttpWebRequestExtensions
{
    private static readonly string[] RestrictedHeaders =
    {
        "Accept",
        "Connection",
        "Content-Length",
        "Content-Type",
        "Date",
        "Expect",
        "Host",
        "If-Modified-Since",
        "Keep-Alive",
        "Proxy-Connection",
        "Range",
        "Referer",
        "Transfer-Encoding",
        "User-Agent"
    };

    private static readonly Dictionary<string, PropertyInfo> HeaderProperties =
        new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);

    static HttpWebRequestExtensions()
    {
        var type = typeof(HttpWebRequest);
        foreach (var header in RestrictedHeaders)
        {
            var propertyName = header.Replace("-", "");
            var headerProperty = type.GetProperty(propertyName);
            HeaderProperties[header] = headerProperty;
        }
    }

    public static void SetRawHeader(this HttpWebRequest request, string name, string value)
    {
        if (HeaderProperties.ContainsKey(name))
        {
            var property = HeaderProperties[name];
            if (property.PropertyType == typeof(DateTime))
                property.SetValue(request, DateTime.Parse(value), null);
            else if (property.PropertyType == typeof(bool))
                property.SetValue(request, bool.Parse(value), null);
            else if (property.PropertyType == typeof(long))
                property.SetValue(request, long.Parse(value), null);
            else
                property.SetValue(request, value, null);
        }
        else
        {
            request.Headers[name] = value;
        }
    }
}