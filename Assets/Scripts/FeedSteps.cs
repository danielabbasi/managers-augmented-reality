using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable()]
public sealed class FeedSteps<TFeedType> : EntityBase
    where TFeedType : EntityBase
{
    private FeedSteps() { }

    #region Variables



    #endregion

    #region Properties

    [JsonProperty("generator")]
    public string Generator { get; set; }

    [JsonProperty("entry")]
    public TFeedType Entries { get; set; }

    #endregion

    #region Methods



    #endregion
}
