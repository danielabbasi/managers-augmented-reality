using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Assets.Scripts;
using System.Linq;
using System;

public class clickLoadProject : MonoBehaviour {

    private ApiHelper ApiHelper { get; set; }
    private Credentials Credentials { get; set; }
    public GameObject processRow;


    public clickLoadProject() {
        Credentials = new Credentials("cristiano.bellucci.fujitsu+cardiffadmin@gmail.com", "Millennium");
    }

    void OnMouseDown(){

        if (ApiHelper == null)
        {
            ApiHelper = new ApiHelper(Credentials.Username, Credentials.Password);
        }


		GameObject projRow= this.gameObject;
        string projName = projRow.transform.Find("project-name").GetComponent<Text>().text;
		GameObject dept = this.gameObject.transform.parent.parent.parent.gameObject;
		GameObject deptOverview = dept.transform.Find ("DeptOverview").gameObject;
		GameObject projOverview = dept.transform.Find("ProjOverview").gameObject;

        setNewViewData(projOverview,projName);

		setCanvasGroup (0f, deptOverview);
		setCanvasGroup (1f, projOverview);

		setToView (projOverview);
		setToHidden(deptOverview);
	}

    void setNewViewData(GameObject projOverview, string project)
    {
        //Add Project Title
        projOverview.transform.Find("Top Container").Find("Dept Title").GetComponent<Text>().text = project;
        //Add Project Members
        GameObject memContainer = projOverview.transform.Find("Member Container").gameObject;


        memContainer.transform.Find("member_1").GetComponent<Text>().text ="Alan Smith";
        memContainer.transform.Find("member_2").GetComponent<Text>().text = "Tim Pike";
        memContainer.transform.Find("member_3").GetComponent<Text>().text = "Emma Stone";


        //Add Project Processes
        GameObject midContainer = projOverview.transform.Find("Middle Container").gameObject;

        ApiUpdateUI(midContainer,project);

    }

    void setCanvasGroup(float alpha, GameObject o){
		CanvasGroup canvas = o.GetComponent ("CanvasGroup") as CanvasGroup;
		canvas.alpha = alpha;
	}

	void setToView(GameObject o){
		o.transform.localPosition = new Vector3(0,0,0);
	}

	void setToHidden(GameObject o){
		o.transform.localPosition = new Vector3(0,0,-4);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ApiUpdateUI(GameObject midContainer, string project) {
        string department;

        if (project.Contains("HR"))
        {
            department = "HR"; 
        }
        else if (project.Contains("Market"))
        {
            department = "Marketing";
        }
        else {
            department = "Engineering";
        }

        switch (department)
        {
            case "HR":
                {
                    AsyncWorker<ProjectsOverviewData>.Dispatch((worker) =>
                    {
                        Feed<OrganisationalUnit> organisationalUnits = ApiHelper.GetOrganisationalUnits();

                        Feed<OrganisationalUnitProcessType> organisationalUnitProcessTypes = ApiHelper.GetOrganisationalUnitProcessTypes();

                        Feed<OrganisationalUnitProcess> organisationalUnitProcesses1 = ApiHelper.GetHROffboardingOrganisationalUnitProcesses();

                        Feed<OrganisationalUnitProcess> organisationalUnitProcesses2 = ApiHelper.GetHROnboardingOrganisationalUnitProcesses();



                        List<ProcessStatuses> processStatuses1 = organisationalUnitProcesses1.Entries
                            .Select((organisationalUnitProcess) =>
                            {
                                return (ProcessStatuses)Convert.ToInt32(organisationalUnitProcess.Categories
                                    .Where((organisationalUnitProcessCategory) => organisationalUnitProcessCategory.Term.ToLower() == "status")
                                    .FirstOrDefault()
                                    .Label);
                            })
                            .ToList();

                        List<ProcessStatuses> processStatuses2 = organisationalUnitProcesses2.Entries
                            .Select((organisationalUnitProcess) =>
                            {
                                return (ProcessStatuses)Convert.ToInt32(organisationalUnitProcess.Categories
                                    .Where((organisationalUnitProcessCategory) => organisationalUnitProcessCategory.Term.ToLower() == "status")
                                    .FirstOrDefault()
                                    .Label);
                            })
                            .ToList();

                        int Process1Error = processStatuses1
                                .Where((processStatus) => processStatus == ProcessStatuses.TechnicalError)
                                .Count();

                        int Process2Error = processStatuses2
                                .Where((processStatus) => processStatus == ProcessStatuses.TechnicalError)
                                .Count();

                        int[] InstancesInError = { Process1Error, Process2Error };

                        Feed<OrganisationalUnitProcess> Processes = null;
                        if (project.ToLower().Contains("offboarding"))
                        {
                            Processes = organisationalUnitProcesses1;
                        }
                        else if (project.ToLower().Contains("onboarding"))
                        {
                            Processes = organisationalUnitProcesses2;
                        }

                        List<int> processStatusList = getProcessStatusList(Processes);

                        ProjectsOverviewData deptData = new ProjectsOverviewData()
                        {
                            ProjectProcesses = Processes,
                            ProcessStatus = processStatusList,
                            InstanceCount = InstancesInError
                        };



                        worker.ReportProgress(deptData);


                    }, (callback) =>
                    {
                     
                            AddProcesses("HR", callback);
                           
                        
                        //Do whatever with the values here

                    }).RunAsync();

                    break;
                }

            case "Marketing":
                {
                    AsyncWorker<ProjectsOverviewData>.Dispatch((worker) =>
                    {
                        Feed<OrganisationalUnit> organisationalUnits = ApiHelper.GetOrganisationalUnits();

                        Feed<OrganisationalUnitProcessType> organisationalUnitProcessTypes = ApiHelper.GetOrganisationalUnitProcessTypes();

                        Feed<OrganisationalUnitProcess> organisationalUnitProcesses1 = ApiHelper.GetMarketingFairOrganisationalUnitProcesses();

                        Feed<OrganisationalUnitProcess> organisationalUnitProcesses2 = ApiHelper.GetMarketingCampaignOrganisationalUnitProcesses();

                        List<ProcessStatuses> processStatuses1 = organisationalUnitProcesses1.Entries
                            .Select((organisationalUnitProcess) =>
                            {
                                return (ProcessStatuses)Convert.ToInt32(organisationalUnitProcess.Categories
                                    .Where((organisationalUnitProcessCategory) => organisationalUnitProcessCategory.Term.ToLower() == "status")
                                    .FirstOrDefault()
                                    .Label);
                            })
                            .ToList();

                        List<ProcessStatuses> processStatuses2 = organisationalUnitProcesses2.Entries
                            .Select((organisationalUnitProcess) =>
                            {
                                return (ProcessStatuses)Convert.ToInt32(organisationalUnitProcess.Categories
                                    .Where((organisationalUnitProcessCategory) => organisationalUnitProcessCategory.Term.ToLower() == "status")
                                    .FirstOrDefault()
                                    .Label);
                            })
                            .ToList();

                        int Process1Error = processStatuses1
                                .Where((processStatus) => processStatus == ProcessStatuses.TechnicalError)
                                .Count();

                        int Process2Error = processStatuses2
                                .Where((processStatus) => processStatus == ProcessStatuses.TechnicalError)
                                .Count();

                        int[] InstancesInError = { Process1Error, Process2Error };


                        Feed<OrganisationalUnitProcess> Processes = null;
                        if (project.ToLower().Contains("fair"))
                        {
                            Processes = organisationalUnitProcesses1;
                        }
                        else if (project.ToLower().Contains("campaign"))
                        {
                            Processes = organisationalUnitProcesses2;
                        }

                        List<int> processStatusList = getProcessStatusList(Processes);

                        ProjectsOverviewData deptData = new ProjectsOverviewData()
                        {
                            ProjectProcesses = Processes,
                            ProcessStatus = processStatusList,
                            InstanceCount = InstancesInError
                        };

                        worker.ReportProgress(deptData);


                    }, (callback) =>
                    {
                        
                            AddProcesses("Marketing", callback);
                        
                        //Do whatever with the values here

                    }).RunAsync();

                    break;
                }

            case "Engineering":
                {
                    AsyncWorker<ProjectsOverviewData>.Dispatch((worker) =>
                    {
                        Feed<OrganisationalUnit> organisationalUnits = ApiHelper.GetOrganisationalUnits();

                        Feed<OrganisationalUnitProcessType> organisationalUnitProcessTypes = ApiHelper.GetOrganisationalUnitProcessTypes();

                        Feed<OrganisationalUnitProcess> organisationalUnitProcesses1 = ApiHelper.GetEngineeringProjectOrganisationalUnitProcesses();

                        Feed<OrganisationalUnitProcess> organisationalUnitProcesses2 = ApiHelper.GetEngineeringReleaseOrganisationalUnitProcesses();

                        Feed<OrganisationalUnitProcess> organisationalUnitProcesses3 = ApiHelper.GetEngineeringTestOrganisationalUnitProcesses();




                        List<ProcessStatuses> processStatuses1 = organisationalUnitProcesses1.Entries
                            .Select((organisationalUnitProcess) =>
                            {
                                return (ProcessStatuses)Convert.ToInt32(organisationalUnitProcess.Categories
                                    .Where((organisationalUnitProcessCategory) => organisationalUnitProcessCategory.Term.ToLower() == "status")
                                    .FirstOrDefault()
                                    .Label);
                            })
                            .ToList();

                        List<ProcessStatuses> processStatuses2 = organisationalUnitProcesses2.Entries
                            .Select((organisationalUnitProcess) =>
                            {
                                return (ProcessStatuses)Convert.ToInt32(organisationalUnitProcess.Categories
                                    .Where((organisationalUnitProcessCategory) => organisationalUnitProcessCategory.Term.ToLower() == "status")
                                    .FirstOrDefault()
                                    .Label);
                            })
                            .ToList();

                        List<ProcessStatuses> processStatuses3 = organisationalUnitProcesses3.Entries
                            .Select((organisationalUnitProcess) =>
                            {
                                return (ProcessStatuses)Convert.ToInt32(organisationalUnitProcess.Categories
                                    .Where((organisationalUnitProcessCategory) => organisationalUnitProcessCategory.Term.ToLower() == "status")
                                    .FirstOrDefault()
                                    .Label);
                            })
                            .ToList();


                        int Process1Error = processStatuses1
                                .Where((processStatus) => processStatus == ProcessStatuses.TechnicalError)
                                .Count();

                        int Process2Error = processStatuses2
                                .Where((processStatus) => processStatus == ProcessStatuses.TechnicalError)
                                .Count();

                        int Process3Error = processStatuses3
                                .Where((processStatus) => processStatus == ProcessStatuses.TechnicalError)
                                .Count();


                        int[] InstancesInError = { Process1Error, Process2Error, Process3Error };


                        Feed<OrganisationalUnitProcess> Processes = null;
                        if (project.ToLower().Contains("project"))
                        {
                            Processes = organisationalUnitProcesses1;
                        }
                        else if (project.ToLower().Contains("release"))
                        {
                            Processes = organisationalUnitProcesses2;
                        }
                        else if (project.ToLower().Contains("test"))
                        {
                            Processes = organisationalUnitProcesses3;
                        }

                        List<int> processStatusList = getProcessStatusList(Processes);

                        
                        foreach (var a in processStatusList) {
                            Debug.Log(a);
                        }

                        ProjectsOverviewData deptData = new ProjectsOverviewData()
                        {
                            ProjectProcesses = Processes,
                            ProcessStatus = processStatusList,
                            InstanceCount = InstancesInError
                        };

                       

                        worker.ReportProgress(deptData);


                    }, (callback) =>
                    {
                        
                            AddProcesses("Engineering", callback);
                       
                       
                        //Do whatever with the values here

                    }).RunAsync();

                    break;
                }
        }

    }

    void AddProcesses(string dept, ProjectsOverviewData Processes) {
        bool ok = false;
        bool wait = false;
        bool error = false;

        GameObject deptObj = GameObject.Find(dept);
        GameObject projectOverview = deptObj.transform.Find("ProjOverview").gameObject;
        GameObject processContainer = projectOverview.transform.Find("Middle Container").gameObject;

        removeExistingRows(processContainer);

        int height = 400;
        
        int Count = 1;
        foreach (var a in Processes.ProjectProcesses.Entries)
        {
            //Set rows position 
            GameObject row = Instantiate(processRow) as GameObject;
            row.transform.SetParent(processContainer.transform);
            row.transform.localPosition = new Vector3(0, -(60 * Count), 0);
            row.transform.localRotation = Quaternion.identity;
            row.transform.localScale = new Vector3(1, 1, 1);
            
            //set row data
            row.transform.Find("proc-name").GetComponent<Text>().text = a.Id;

            Debug.Log(Count+"----"+Processes.ProcessStatus.ElementAt(Count-1));
            if (Processes.ProcessStatus.ElementAt(Count -1) == 301) {
                row.transform.Find("proc-status").GetComponent<UnityEngine.UI.Image>().color = new Color32(220, 0, 0, 255);
                error = true;
            }
            else if (Processes.ProcessStatus.ElementAt(Count-1) == 201) {
                row.transform.Find("proc-status").GetComponent<UnityEngine.UI.Image>().color = new Color32(15, 225, 0, 255);
                ok = true;
            }
            else if (Processes.ProcessStatus.ElementAt(Count-1) == 102) {
                row.transform.Find("proc-status").GetComponent<UnityEngine.UI.Image>().color = new Color32(220, 150, 0, 255);
                wait = true;
            }

            //row.transform.Find("proc-sie").GetComponent<Text>().text = callback.InstanceCount[Count - 1].ToString();

            //Extend dept canvas
            projectOverview.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,400 + (Count*60));


            Count++;
        }

        if (error == true) {
            deptObj.transform.Find("ProjOverview").transform.Find("Top Container").transform.Find("Dept Status").GetComponent<UnityEngine.UI.Image>().color = new Color32(220, 0, 0, 255);
        }
        else if (error == false){
            if (wait == true) {
                deptObj.transform.Find("ProjOverview").transform.Find("Top Container").transform.Find("Dept Status").GetComponent<UnityEngine.UI.Image>().color = new Color32(220, 150, 0, 255);
            }
            else if (wait == false) {
                if (ok == true) {
                    deptObj.transform.Find("ProjOverview").transform.Find("Top Container").transform.Find("Dept Status").GetComponent<UnityEngine.UI.Image>().color = new Color32(15, 225, 0, 255);
                }
                else {
                    deptObj.transform.Find("ProjOverview").transform.Find("Top Container").transform.Find("Dept Status").GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 255, 255, 255);
                }
            }
        }

    }

    void removeExistingRows(GameObject processContainer) {

        foreach (Transform child in processContainer.transform)
        {
            if (child.name.Contains("Clone")) {
                GameObject.Destroy(child.gameObject);
            }
        }

    }

    private List<int> getProcessStatusList(Feed<OrganisationalUnitProcess> Processes) {
        List<int> statusList = new List<int>();
        foreach (var a in Processes.Entries)
        {
            if (a.Categories != null || a.Categories.Count > 0)
            {
                int status = Int32.Parse(a.Categories.First().Label);
                statusList.Add(status);
            }
            else
            {
                Debug.Log("FAIL");
            }

        }

        return statusList ;
    }

   
}
