using Assets.StepsData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickLoadProcess : MonoBehaviour {

    private ApiHelperReturnJSON ApiHelper { get; set; }
    private Credentials Credentials { get; set; }

    public GameObject stepRow;

    public clickLoadProcess()
    {
        Credentials = new Credentials("cristiano.bellucci.fujitsu+cardiffadmin@gmail.com", "Millennium");
    }

    void OnMouseDown(){

        if (ApiHelper == null)
        {
            ApiHelper = new ApiHelperReturnJSON(Credentials.Username, Credentials.Password);
        }

        Debug.Log (this.gameObject.name + " Was Clicked.");
		string procName = this.gameObject.transform.Find("proc-name").GetComponent<Text>().text;
		GameObject dept = this.gameObject.transform.parent.parent.parent.gameObject;
		GameObject processOverview = dept.transform.Find ("ProcessOverview").gameObject;
		GameObject projOverview = dept.transform.Find("ProjOverview").gameObject;
       

        setNewViewData(processOverview,procName);

        setCanvasGroup (0f, projOverview);
		setCanvasGroup (1f, processOverview);

		setToView (processOverview);
		setToHidden(projOverview);
	}

    void setNewViewData(GameObject procOverview, string process)
    {
        //Add Project Title
        procOverview.transform.Find("Top Container").Find("Dept Title").GetComponent<Text>().text = process;
        Debug.Log(procOverview.name);
        Debug.Log(process);



        AsyncWorker<StepsData>.Dispatch((worker) =>
        {
            StepsData stepD = ApiHelperExtensions.GetProcessStepsById(ApiHelper, process);


        worker.ReportProgress(stepD);


        }, (callback) =>
            {
                AddSteps(callback);
        }).RunAsync();
}

    void AddSteps(StepsData stepD) {
        bool ok = false;
        bool wait = false;
        bool error = false;
        GameObject deptObj = this.gameObject.transform.parent.parent.parent.gameObject;
        GameObject processOverview = deptObj.transform.Find("ProcessOverview").gameObject;
        GameObject processContainer = processOverview.transform.Find("Middle Container").gameObject;

        removeExistingRows(processContainer);

        int height = 400;

        int Count = 1;
        foreach (var a in stepD.feed.entry.content.P_value.path) {
            Debug.Log(a.id + "- Status Code: "+a.st);

            //Set rows position 
            GameObject row = Instantiate(stepRow) as GameObject;
            row.transform.SetParent(processContainer.transform);
            row.transform.localPosition = new Vector3(0, -(60 * Count), 0);
            row.transform.localRotation = Quaternion.identity;
            row.transform.localScale = new Vector3(1, 1, 1);

            //set row data
            row.transform.Find("step-name").GetComponent<Text>().text = a.st.ToString();
            //row.transform.Find("proc-sie").GetComponent<Text>().text = callback.InstanceCount[Count - 1].ToString();
            if (a.st == 301)
            {
                row.transform.Find("step-status").GetComponent<UnityEngine.UI.Image>().color = new Color32(220, 0, 0, 255);
                error = true;
            }
            else if (a.st == 201)
            {
                row.transform.Find("step-status").GetComponent<UnityEngine.UI.Image>().color = new Color32(15, 225, 0, 255);
                ok = true;
            }
            else if (a.st == 102)
            {
                row.transform.Find("step-status").GetComponent<UnityEngine.UI.Image>().color = new Color32(220, 150, 0, 255);
                wait = true;
            }
            //Extend dept canvas
            processOverview.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 400 + (Count * 60));

            Count++;

        }

        if (error == true)
        {
            deptObj.transform.Find("ProcessOverview").transform.Find("Top Container").transform.Find("Dept Status").GetComponent<UnityEngine.UI.Image>().color = new Color32(220, 0, 0, 255);
        }
        else if (error == false)
        {
            if (wait == true)
            {
                deptObj.transform.Find("ProcessOverview").transform.Find("Top Container").transform.Find("Dept Status").GetComponent<UnityEngine.UI.Image>().color = new Color32(220, 150, 0, 255);
            }
            else if (wait == false)
            {
                if (ok == true)
                {
                    deptObj.transform.Find("ProcessOverview").transform.Find("Top Container").transform.Find("Dept Status").GetComponent<UnityEngine.UI.Image>().color = new Color32(15, 225, 0, 255);
                }
                else
                {
                    deptObj.transform.Find("ProcessOverview").transform.Find("Top Container").transform.Find("Dept Status").GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 255, 255, 255);
                }
            }
        }
    }

    void removeExistingRows(GameObject stepsContainer)
    {

        foreach (Transform child in stepsContainer.transform)
        {
            if (child.name.Contains("Clone"))
            {
                GameObject.Destroy(child.gameObject);
            }
        }

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
}
