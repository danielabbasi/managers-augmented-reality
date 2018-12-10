using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using ChartAndGraph;

public class PopDeptOverview : MonoBehaviour, ITrackableEventHandler
{
	public Font font;

	private bool uiUpdated = false;

    public PopDeptOverview()
    {
        Credentials = new Credentials("cristiano.bellucci.fujitsu+cardiffadmin@gmail.com", "Millennium");
    }


    #region Variables

    private TrackableBehaviour _trackableBehaviour;

    #endregion

    #region Properties

    private ApiHelper ApiHelper { get; set; }

    private Credentials Credentials { get; set; }

    #endregion

    #region Methods

    public void Start()
    {
        _trackableBehaviour = GetComponent<TrackableBehaviour>();

        if (_trackableBehaviour)
        {
            _trackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            //Image target is found

            if (ApiHelper == null)
            {
                ApiHelper = new ApiHelper(Credentials.Username, Credentials.Password);
            }

            switch (_trackableBehaviour.name)
            {
                case "HR":
                    {
                        AsyncWorker<OrganisationalUnitStatuses>.Dispatch((worker) =>
                        {
                            Feed<OrganisationalUnit> organisationalUnits = ApiHelper.GetOrganisationalUnits();

                            Feed<OrganisationalUnitProcessType> organisationalUnitProcessTypes = ApiHelper.GetOrganisationalUnitProcessTypes();

                            Feed<OrganisationalUnitProcess> organisationalUnitProcesses1 = ApiHelper.GetOrganisationalUnitProcesses(
                                organisationalUnitProcessTypes.Entries
                                    .Where((organisationalUnitProcessType) => organisationalUnitProcessType.Title.ToLower() == "cwl hr offboarding")
                                    .FirstOrDefault()
                                    .Id);

                            Feed<OrganisationalUnitProcess> organisationalUnitProcesses2 = ApiHelper.GetOrganisationalUnitProcesses(
                                organisationalUnitProcessTypes.Entries
                                    .Where((organisationalUnitProcessType) => organisationalUnitProcessType.Title.ToLower() == "cwl hr onboarding")
                                    .FirstOrDefault()
                                    .Id);

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

                            List<ProcessStatuses> processStatuses = new List<ProcessStatuses>();

                            processStatuses.AddRange(processStatuses1);
                            processStatuses.AddRange(processStatuses2);

                            worker.ReportProgress(new OrganisationalUnitStatuses(
                                processStatuses
                                    .Where((processStatus) => processStatus == ProcessStatuses.TechnicalError)
                                    .Count(),
                                    processStatuses
                                        .Where((processStatus) => processStatus == ProcessStatuses.Ok)
                                        .Count(),
                                    processStatuses
                                        .Where((processStatus) => processStatus == ProcessStatuses.Awaiting)
                                        .Count()));
                        }, (callback) =>
                        {
                            //Do whatever with the values here

//                            pieChartHR.DataSource.SetValue("Successful", callback.OkCount);
//                            pieChartHR.DataSource.SetValue("Pending", callback.AwaitingCount);
//                            pieChartHR.DataSource.SetValue("Technical Error", callback.TechnicalErrorCount);
							if (uiUpdated == false) {
								updateUI("HR",callback);
							}
                            Debug.Log("Error Count: " + callback.TechnicalErrorCount);
                            Debug.Log("OK Count: " + callback.OkCount);
                            Debug.Log("Pending Count: " + callback.AwaitingCount);
                        }).RunAsync();

                        break;
                    }
                case "Engineering":
                    {
                        AsyncWorker<OrganisationalUnitStatuses>.Dispatch((worker) =>
                        {
                            Feed<OrganisationalUnit> organisationalUnits = ApiHelper.GetOrganisationalUnits();

                            Feed<OrganisationalUnitProcessType> organisationalUnitProcessTypes = ApiHelper.GetOrganisationalUnitProcessTypes();

                            Feed<OrganisationalUnitProcess> organisationalUnitProcesses = ApiHelper.GetOrganisationalUnitProcesses(
                                organisationalUnitProcessTypes.Entries
                                    .Where((organisationalUnitProcessType) => organisationalUnitProcessType.Title.ToLower() == "cwl hr offboarding")
                                    .FirstOrDefault()
                                    .Id);

                            List<ProcessStatuses> processStatuses = organisationalUnitProcesses.Entries
                                .Select((organisationalUnitProcess) =>
                                {
                                    return (ProcessStatuses)Convert.ToInt32(organisationalUnitProcess.Categories
                                        .Where((organisationalUnitProcessCategory) => organisationalUnitProcessCategory.Term.ToLower() == "status")
                                        .FirstOrDefault()
                                        .Label);
                                })
                                .ToList();

                            worker.ReportProgress(new OrganisationalUnitStatuses(
                                processStatuses
                                    .Where((processStatus) => processStatus == ProcessStatuses.TechnicalError)
                                    .Count(),
                                    processStatuses
                                        .Where((processStatus) => processStatus == ProcessStatuses.Ok)
                                        .Count(),
                                    processStatuses
                                        .Where((processStatus) => processStatus == ProcessStatuses.Awaiting)
                                        .Count()));
                        }, (callback) =>
                        {
                            //Do whatever with the values here
							if (uiUpdated == false) {
								updateUI("Engineering",callback);
							}
                            Debug.Log(callback.TechnicalErrorCount);
                            Debug.Log(callback.OkCount);
                            Debug.Log(callback.AwaitingCount);
                        }).RunAsync();

                        break;
                    }
                case "Marketing":
                    {
                        AsyncWorker<OrganisationalUnitStatuses>.Dispatch((worker) =>
                        {
                            Feed<OrganisationalUnit> organisationalUnits = ApiHelper.GetOrganisationalUnits();

                            Feed<OrganisationalUnitProcessType> organisationalUnitProcessTypes = ApiHelper.GetOrganisationalUnitProcessTypes();

                            Feed<OrganisationalUnitProcess> organisationalUnitProcesses = ApiHelper.GetOrganisationalUnitProcesses(
                                organisationalUnitProcessTypes.Entries
                                    .Where((organisationalUnitProcessType) => organisationalUnitProcessType.Title.ToLower() == "cwl hr offboarding")
                                    .FirstOrDefault()
                                    .Id);

                            List<ProcessStatuses> processStatuses = organisationalUnitProcesses.Entries
                                .Select((organisationalUnitProcess) =>
                                {
                                    return (ProcessStatuses)Convert.ToInt32(organisationalUnitProcess.Categories
                                        .Where((organisationalUnitProcessCategory) => organisationalUnitProcessCategory.Term.ToLower() == "status")
                                        .FirstOrDefault()
                                        .Label);
                                })
                                .ToList();

                            worker.ReportProgress(new OrganisationalUnitStatuses(
                                processStatuses
                                    .Where((processStatus) => processStatus == ProcessStatuses.TechnicalError)
                                    .Count(),
                                    processStatuses
                                        .Where((processStatus) => processStatus == ProcessStatuses.Ok)
                                        .Count(),
                                    processStatuses
                                        .Where((processStatus) => processStatus == ProcessStatuses.Awaiting)
                                        .Count()));
                        }, (callback) =>
                        {
                            //Do whatever with the values here
							if (uiUpdated == false) {
								Debug.Log(callback.GetType());
								updateUI("Marketing",callback);
							}
                            Debug.Log(callback.TechnicalErrorCount);
                            Debug.Log(callback.OkCount);
                            Debug.Log(callback.AwaitingCount);
                        }).RunAsync();

                        break;
                    }
            }

        }
        else
        {
            //Image target is lost


        }
    }

	private void updateUI(String deptStr, OrganisationalUnitStatuses callback)
	{
		GameObject dept = GameObject.Find(deptStr);
		GameObject deptOverview = dept.transform.Find("DeptOverview").gameObject;
		GameObject topContainer = deptOverview.transform.Find("Top Container").gameObject;
		GameObject titleContainer = topContainer.transform.Find("Dept Title").gameObject;
		titleContainer.transform.SetParent(topContainer.transform);
		setTitle (titleContainer, deptStr);
		addProjects ();
//		CanvasGroup cg = deptOverview.GetComponent("CanvasGroup") as CanvasGroup;
//		cg.alpha = 0.1f;
		uiUpdated = true;
	}

	private void setTitle(GameObject titleContainer, String deptStr){
		Text myText = titleContainer.AddComponent<Text>();
		myText.font = font;
		myText.text = deptStr;
		myText.alignment = TextAnchor.MiddleCenter;
		myText.resizeTextForBestFit = true;
	}

	private void addProjects(){

	}

    #endregion
}