using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable()]
public sealed class OrganisationalUnitProcess : EntityBase
{
    private OrganisationalUnitProcess() { }

    #region Variables
    
    
    
    #endregion
    
    #region Properties

    /// <summary>
    /// The ID of the entry.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// The name of the entry.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>
    /// The <see cref="DateTime"/> representing when the entry was last updated.
    /// </summary>
    [JsonProperty("updated")]
    public DateTime Updated { get; set; }

    /// <summary>
    /// The <see cref="DateTime"/> representing when the entry was initially published.
    /// </summary>
    [JsonProperty("published")]
    public DateTime Published { get; set; }

    [JsonProperty("category")]
    public List<Category> Categories { get; set; }

    #endregion
    
    #region Methods
    
    
    
    #endregion
}