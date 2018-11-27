using System;

public static class ApiHelperExtensions
{
    #region Variables
    
    
    
    #endregion
    
    #region Properties
    
    
    
    #endregion
    
    #region Methods

    public static Feed<OrganisationalUnit> GetOrganisationalUnits(this ApiHelper helper)
    {
        if (helper == null) throw new ArgumentNullException("helper");
        
        return helper.PerformRequest<Feed<OrganisationalUnit>>(
            @"https://live.runmyprocess.com/config/112761542179152739/lane/?nb=@number&orderby=@orderBy&order=@order&filter=@filter&operator=@operator&value=@value",
            (values) =>
            {
                values.Add("number", "20");
                values.Add("orderBy", Filters.Name);
                values.Add("order", Orders.Ascending);
                values.Add("filter", "POOL_NAME");
                values.Add("operator", "CONTAINS");
                values.Add("value", "cwl");
            });
    }

    public static Feed<OrganisationalUnitProcessType> GetOrganisationalUnitProcessTypes(this ApiHelper helper)
    {
        if (helper == null) throw new ArgumentNullException("helper");
        
        return helper.PerformRequest<Feed<OrganisationalUnitProcessType>>(
            @"https://live.runmyprocess.com/config/112761542179152739/user/993079/project/?filter=@filter&operator=@operator&value=@value",
            (values) =>
            {
                values.Add("order", Orders.Ascending);
                values.Add("filter", "NAME");
                values.Add("operator", "CONTAINS");
                values.Add("value", "cwl");
            });
    }

    public static Feed<OrganisationalUnitProcess> GetOrganisationalUnitProcesses(this ApiHelper helper, long id)
    {
        if (helper == null) throw new ArgumentNullException("helper");
        
        return helper.PerformRequest<Feed<OrganisationalUnitProcess>>(
            @"https://live.runmyprocess.com/live/112761542179152739/request?operator=@operator&column=@column&value=@value&filter=@filter&nb=@number&first=@first&method=GET&P_rand=82368",
            (values) =>
            {
                values.Add("operator", "EE EE IS");
                values.Add("column", "name status events published updated");
                values.Add("value", Convert.ToString(id) + " TEST NULL");
                values.Add("filter", "PROJECT MODE PARENT");
                values.Add("number", Convert.ToString(20));
                values.Add("first", Convert.ToString(0));
            });
    }

    #endregion
}