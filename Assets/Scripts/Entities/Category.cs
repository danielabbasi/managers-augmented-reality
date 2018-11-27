using System;
using Newtonsoft.Json;

[Serializable()]
public sealed class Category : EntityBase
{
    private Category() { }

    #region Variables
    
    
    
    #endregion
    
    #region Properties

    [JsonProperty("@term")]
    public string Term { get; set; }

    [JsonProperty("@label")]
    public string Label { get; set; }

    #endregion
    
    #region Methods
    
    
    
    #endregion
}