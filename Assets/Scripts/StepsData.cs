using System;
using System.Collections.Generic;
using Newtonsoft.Json;



namespace Assets.StepsData
{
    [Serializable()]
    public class Summary
    {
        [JsonProperty("type")]
        public string type { get; set; }
    }

    [Serializable()]
    public class Category
    {
        [JsonProperty("term")]
        public string term { get; set; }
    }

    [Serializable()]
    public class Loop
    {
        [JsonProperty("st")]
        public int st { get; set; }
        [JsonProperty("cnt")]
        public int cnt { get; set; }
        [JsonProperty("end")]
        public long? end { get; set; }
        [JsonProperty("loopback")]
        public int loopback { get; set; }
    }

    [Serializable()]
    public class Path
    {
        [JsonProperty("st")]
        public long? st { get; set; }
        [JsonProperty("loop")]
        public List<Loop> loop { get; set; }
        [JsonProperty("end")]
        public long? end { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
    }

    [Serializable()]
    public class PValue
    {
        [JsonProperty("path")]
        public List<Path> path { get; set; }
    }

    [Serializable()]
    public class Content
    {
        [JsonProperty("P_value")]
        public PValue P_value { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
    }

    [Serializable()]
    public class Entry
    {
        [JsonProperty("summary")]
        public Summary summary { get; set; }
        [JsonProperty("rights")]
        public string rights { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("published")]
        public DateTime published { get; set; }
        [JsonProperty("title")]
        public string title { get; set; }
        [JsonProperty("category")]
        public Category category { get; set; }
        [JsonProperty("updated")]
        public DateTime updated { get; set; }
        [JsonProperty("content")]
        public Content content { get; set; }
    }

    [Serializable()]
    public class Link
    {
        [JsonProperty("rel")]
        public string rel { get; set; }
        [JsonProperty("href")]
        public string href { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("title")]
        public string title { get; set; }
    }

    [Serializable()]
    public class Category2
    {
        [JsonProperty("term")]
        public string term { get; set; }
        [JsonProperty("label")]
        public string label { get; set; }
    }

    [Serializable()]
    public class Feed
    {
        [JsonProperty("entry")]
        public Entry entry { get; set; }

        //[JsonProperty("rights")]
        //public string rights { get; set; }
        //[JsonProperty("link")]
        //public List<Link> link { get; set; }
        //[JsonProperty("generator")]
        //public string generator { get; set; }
        //[JsonProperty("id")]
        //public string id { get; set; }
        //[JsonProperty("title")]
        //public string title { get; set; }
        //[JsonProperty("category")]
        //public List<Category2> category { get; set; }
        //[JsonProperty("update")]
        //public DateTime updated { get; set; }
        //[JsonProperty("base")]
        //public string @base { get; set; }
    }

    [Serializable()]
    public class StepsData : EntityBase
    {
        [JsonProperty("feed")]
        public Feed feed { get; set; }
    }
}
