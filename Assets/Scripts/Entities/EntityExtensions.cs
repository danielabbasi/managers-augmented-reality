public static class EntityExtensions
{
    #region Variables
    
    
    
    #endregion
    
    #region Properties
    
    
    
    #endregion
    
    #region Methods

    public static string ToStatusString(this ProcessStatuses process)
    {
        switch (process)
        {
            case ProcessStatuses.TechnicalError:
            {
                return "Technical Error";
            }
            case ProcessStatuses.Ok:
            {
                return "OK";
            }
            case ProcessStatuses.Awaiting:
            {
                return "Awaiting";
            }
        }

        return string.Empty;
    }
    
    #endregion
}