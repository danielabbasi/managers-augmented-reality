using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable()]
public sealed class Feed<TFeedType> : EntityBase
    where TFeedType : EntityBase
{
    private Feed() { }

    #region Variables
    
    
    
    #endregion
    
    #region Properties

    [JsonProperty("generator")]
    public string Generator { get; set; }

    [JsonProperty("entry")]
    public List<TFeedType> Entries { get; set; }
    
    #endregion
    
    #region Methods
    
    
    
    #endregion
}