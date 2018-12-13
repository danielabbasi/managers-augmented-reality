using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class ProjectsOverviewData
    {
        public Feed<OrganisationalUnitProcess> ProjectProcesses { get; set; }
        public int[] InstanceCount { get; set; }
    }
}
