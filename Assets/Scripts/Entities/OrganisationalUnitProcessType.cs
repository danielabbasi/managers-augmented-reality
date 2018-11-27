using System;
using Newtonsoft.Json;

[Serializable()]
public sealed class OrganisationalUnitProcessType : EntityBase
{
    private OrganisationalUnitProcessType() { }

    #region Variables
    
    
    
    #endregion
    
    #region Properties

    /// <summary>
    /// The ID of the entry.
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

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

    #endregion
    
    #region Methods
    
    
    
    #endregion
}