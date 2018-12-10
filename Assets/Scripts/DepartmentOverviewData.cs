using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class DepartmentOverviewData
    {

        public Feed<OrganisationalUnitProcessType> OrganisationalUnitProcessType { get;set;}
        public int[] InstanceCount { get; set; }
        //public int InstanceBCount { get; set; }

    }
}
