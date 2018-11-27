public sealed class OrganisationalUnitStatuses
{
    public OrganisationalUnitStatuses(long technicalErrorCount, long okCount, long awaitingCount)
    {
        TechnicalErrorCount = technicalErrorCount;
        OkCount = okCount;
        AwaitingCount = awaitingCount;
    }
 
    #region Variables
 
 
    #endregion
 
    #region Properties
 
    public long TechnicalErrorCount { get; private set; }
 
    public long OkCount { get; private set; }
 
    public long AwaitingCount { get; private set; }
 
    #endregion
 
    #region Methods
 
 
    #endregion
}