using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using Newtonsoft.Json;

public static class ExtensionMethods
{
    #region Variables
    
    
    
    #endregion
    
    #region Properties
    
    
    
    #endregion
    
    #region Methods

    public static string ToJson(this XmlDocument document)
    {
        if (document == null) throw new ArgumentNullException("document");

        return JsonConvert.SerializeXmlNode(document);
    }

    public static Dictionary<string, string> AddIfNonExistent(this Dictionary<string, string> dictionary, KeyValuePair<string, string> keyValue)
    {
        string value = null;

        return dictionary.AddIfNonExistent(keyValue, out value);
    }
    
    public static Dictionary<string, string> AddIfNonExistent(this Dictionary<string, string> dictionary, KeyValuePair<string, string> keyValue, out string value)
    {
        if (dictionary == null) throw new ArgumentNullException("dictionary");

        if (dictionary.TryGetValue(keyValue.Key, out value) == false)
        {
            dictionary.Add(keyValue.Key, keyValue.Value);
        }

        return dictionary;
    }

    #endregion
}