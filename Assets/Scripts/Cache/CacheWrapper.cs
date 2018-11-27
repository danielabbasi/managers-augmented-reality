using System.Collections.Generic;

public sealed class CacheWrapper<TBaseType>
    where TBaseType : class
{
    internal CacheWrapper()
    {
        UnderlyingCache = new Dictionary<string, object>();
    }
    
    #region Variables
    
    
    
    #endregion
    
    #region Properties

    private Dictionary<string, object> UnderlyingCache { get; set; }

    #endregion
    
    #region Methods

    public TEntityType Get<TEntityType>(string key)
        where TEntityType : class, TBaseType
    {
        object value = null;
        
        if (UnderlyingCache.TryGetValue(key, out value) == true)
        {
            return (TEntityType) value;
        }

        return null;
    }

    public TEntityType Set<TEntityType>(string key, TEntityType value)
        where TEntityType : TBaseType
    {
        if (UnderlyingCache.ContainsKey(key) == false)
        {
            UnderlyingCache.Add(key, value);
        }

        return value;
    }
    
    #endregion
}