using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using UnityEngine;

public sealed class ApiHelperReturnJSON
{
    internal ApiHelperReturnJSON(string username, string password)
    {
        Username = username;
        Password = password;

        Cache = new CacheWrapper<EntityBase>();
    }

    #region Variables



    #endregion

    #region Properties

    private CacheWrapper<EntityBase> Cache { get; set; }

    public string Username { get; private set; }

    private string Password { get; set; }

    #endregion

    #region Methods

    public TEntityType PerformRequest<TEntityType>(string url, Action<Dictionary<string, string>> parameters)
        where TEntityType : EntityBase
    {
        if (url == null) throw new ArgumentNullException("url");

        if (parameters == null) throw new ArgumentNullException("parameters");

        Dictionary<string, string> computedParameters = new Dictionary<string, string>();

        parameters.Invoke(computedParameters);

        computedParameters.AddIfNonExistent(new KeyValuePair<string, string>("method", "GET"));

        computedParameters.AddIfNonExistent(new KeyValuePair<string, string>("P_rand", "29154"));

        //Add computed parameters to API URL

        foreach (KeyValuePair<string, string> keyValue in computedParameters)
        {
            if (url.Contains("@" + keyValue.Key ) || url.EndsWith("@" + keyValue.Key))
            {
                url = url.Replace("@" + keyValue.Key, keyValue.Value);
            }
        }

        Debug.Log(url);
        //Check if this request has been processed before by checking the cache

        TEntityType entity = Cache.Get<TEntityType>(url);

        if (entity != null)
        {
            return entity;
        }

        //Get encoded authentication information

        string authenticationString = Convert.ToBase64String(
            Encoding.GetEncoding("ISO-8859-1").GetBytes(Username + ":" + Password));

        //Perform web request to URL with authentication information.

        ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, policy) => true;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

        request.Headers.Add("Authorization", "Basic " + authenticationString);
        request.SetRawHeader("Accept", "application/json");


        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        if (response.StatusCode != HttpStatusCode.OK)
        {
            //Response was unsuccessful.

            return null;
        }

        using (Stream stream = response.GetResponseStream())
        {
            if (stream != null)
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string xml = reader.ReadToEnd();

                    //The data returned here is in an XML format - we need to first change it to JSON format.

                    

                    TEntityType parsedEntity = JsonConvert.DeserializeObject<TEntityType>(xml);

                    Cache.Set(url, parsedEntity);

                    return parsedEntity;
                }
            }
        }

        return null;
    }



    #endregion
}
