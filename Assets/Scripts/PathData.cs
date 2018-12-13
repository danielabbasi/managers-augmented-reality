using System;
using System.Collections.Generic;
using Newtonsoft.Json;


[Serializable()]
public sealed class PathData : EntityBase
{
    private PathData() { }
    #region Variables



    #endregion

    #region Properties

    /// <summary>
    /// The ID of the entry.
    /// </summary>
    [JsonProperty("path")]
    public PathData PathV { get; set; }



    #endregion

    #region Methods



    #endregion
}
