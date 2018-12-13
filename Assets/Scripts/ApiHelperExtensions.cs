using Assets.Scripts;
using Assets.StepsData;
using System;
using UnityEngine;

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

    public static Feed<OrganisationalUnitProcess> GetMarketingFairOrganisationalUnitProcesses(this ApiHelper helper)
    {
        if (helper == null) throw new ArgumentNullException("helper");

        return helper.PerformRequest<Feed<OrganisationalUnitProcess>>(
            @"https://live.runmyprocess.com/live/112761542179152739/request?operator=EE%20EE%20IS&column=name%20status%20events%20published%20updated&value=215357%20ACCEPTANCE%20NULL&filter=PROJECT%20MODE%20PARENT&nb=20&first=0&method=GET&P_rand=77540",
            (values) => { });
    }

    public static Feed<OrganisationalUnitProcess> GetMarketingCampaignOrganisationalUnitProcesses(this ApiHelper helper)
    {
        if (helper == null) throw new ArgumentNullException("helper");

        return helper.PerformRequest<Feed<OrganisationalUnitProcess>>(
            @"https://live.runmyprocess.com/live/112761542179152739/requestreport/CWL%20Market%20Campaign%20Report.csv?operator=EE%20EE%20IS&column=name%20status%20events%20published%20updated&value=215356%20ACCEPTANCE%20NULL&filter=PROJECT%20MODE%20PARENT&nb=20&first=0&method=GET&P_rand=34765",
            (values) => { });
    }

    public static Feed<OrganisationalUnitProcess> GetEngineeringProjectOrganisationalUnitProcesses(this ApiHelper helper)
    {
        if (helper == null) throw new ArgumentNullException("helper");

        return helper.PerformRequest<Feed<OrganisationalUnitProcess>>(
            @"https://live.runmyprocess.com/live/112761542179152739/request?operator=EE%20EE%20IS&column=name%20status%20events%20published%20updated&value=215449%20ACCEPTANCE%20NULL&filter=PROJECT%20MODE%20PARENT&nb=20&first=0&method=GET&P_rand=80194",
            (values) => { });
    }

    public static Feed<OrganisationalUnitProcess> GetEngineeringReleaseOrganisationalUnitProcesses(this ApiHelper helper)
    {
        if (helper == null) throw new ArgumentNullException("helper");

        return helper.PerformRequest<Feed<OrganisationalUnitProcess>>(
            @"https://live.runmyprocess.com/live/112761542179152739/request?operator=EE%20EE%20IS&column=name%20status%20events%20published%20updated&value=215400%20ACCEPTANCE%20NULL&filter=PROJECT%20MODE%20PARENT&nb=20&first=0&method=GET&P_rand=22715",
            (values) => { });
    }

    public static Feed<OrganisationalUnitProcess> GetEngineeringTestOrganisationalUnitProcesses(this ApiHelper helper)
    {
        if (helper == null) throw new ArgumentNullException("helper");

        return helper.PerformRequest<Feed<OrganisationalUnitProcess>>(
            @"https://live.runmyprocess.com/live/112761542179152739/request?operator=EE%20EE%20IS&column=name%20status%20events%20published%20updated&value=215358%20ACCEPTANCE%20NULL&filter=PROJECT%20MODE%20PARENT&nb=20&first=0&method=GET&P_rand=84319",
            (values) => { });
    }

    public static StepsData GetProcessStepsById(this ApiHelperReturnJSON helper, string id)
    {
        if (helper == null) throw new ArgumentNullException("helper");

        StepsData stepsData = helper.PerformRequest<StepsData>(
            @"https://live.runmyprocess.com/live/112761542179152739/request/@value?P_mode=ACCEPTANCE",
            (values) =>
            {
                values.Add("value", Convert.ToString(id));
            });

       

        return stepsData;
    }





    #endregion
}