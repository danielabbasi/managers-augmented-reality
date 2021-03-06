﻿using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;
using ChartAndGraph;

public class ImageTracker : MonoBehaviour, ITrackableEventHandler
{
    public ImageTracker()
    {
        Credentials = new Credentials("cristiano.bellucci.fujitsu+cardiffadmin@gmail.com", "Millennium");
    }

    public PieChart pieChartHR;
    public PieChart pieChartMar;
    public PieChart pieChartEng;

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

                            pieChartHR.DataSource.SetValue("Successful", callback.OkCount);
                            pieChartHR.DataSource.SetValue("Pending", callback.AwaitingCount);
                            pieChartHR.DataSource.SetValue("Technical Error", callback.TechnicalErrorCount);

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

                            List<ProcessStatuses> processStatuses = new List<ProcessStatuses>();

                            processStatuses.AddRange(processStatuses1);
                            processStatuses.AddRange(processStatuses2);
                            processStatuses.AddRange(processStatuses3);

                            Debug.Log(processStatuses);


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
                            Debug.Log("Error Count: " + callback.TechnicalErrorCount);
                            Debug.Log("OK Count: " + callback.OkCount);
                            Debug.Log("Pending Count: " + callback.AwaitingCount);

                            pieChartEng.DataSource.SetValue("Successful", callback.OkCount);
                            pieChartEng.DataSource.SetValue("Pending", callback.AwaitingCount);
                            pieChartEng.DataSource.SetValue("Technical Error", callback.TechnicalErrorCount);
                         

                        }).RunAsync();

                        break;
                    }
                case "Marketing":

                    {
                        AsyncWorker<OrganisationalUnitStatuses>.Dispatch((worker) =>
                        {
                            Feed<OrganisationalUnit> organisationalUnits = ApiHelper.GetOrganisationalUnits();

                            Feed<OrganisationalUnitProcessType> organisationalUnitProcessTypes = ApiHelper.GetOrganisationalUnitProcessTypes();

                            Feed<OrganisationalUnitProcess> organisationalUnitProcesses = ApiHelper.GetMarketingFairOrganisationalUnitProcesses();

                            Feed<OrganisationalUnitProcess> organisationalUnitProcesses1 = ApiHelper.GetMarketingCampaignOrganisationalUnitProcesses();

                            List<ProcessStatuses> processStatuses1 = organisationalUnitProcesses.Entries
                                .Select((organisationalUnitProcess) =>
                                {
                                    return (ProcessStatuses)Convert.ToInt32(organisationalUnitProcess.Categories
                                        .Where((organisationalUnitProcessCategory) => organisationalUnitProcessCategory.Term.ToLower() == "status")
                                        .FirstOrDefault()
                                        .Label);
                                })
                                .ToList();

                            List<ProcessStatuses> processStatuses2 = organisationalUnitProcesses1.Entries
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

                            Debug.Log("Error Count: " + callback.TechnicalErrorCount);
                            Debug.Log("OK Count: " + callback.OkCount);
                            Debug.Log("Pending Count: " + callback.AwaitingCount);

                            pieChartMar.DataSource.SetValue("Successful", callback.OkCount);
                            pieChartMar.DataSource.SetValue("Pending", callback.AwaitingCount);
                            pieChartMar.DataSource.SetValue("Technical Error", callback.TechnicalErrorCount);

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

	private void updateUI()
	{
	}

    #endregion
}