using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickLoadSteps : MonoBehaviour {

	void OnMouseDown(){
		Debug.Log (this.gameObject.name + " Was Clicked.");
		string stepName = this.gameObject.name;
		GameObject dept = this.gameObject.transform.parent.parent.parent.gameObject;
		GameObject processOverview = dept.transform.Find ("ProcessOverview").gameObject;
		GameObject stepOverview = dept.transform.Find("StepOverview").gameObject;

        setNewViewData(stepOverview,stepName);

        setCanvasGroup (0f, processOverview);
		setCanvasGroup (1f, stepOverview);

		setToView (stepOverview);
		setToHidden(processOverview);
	}

    void setNewViewData(GameObject stepOverview, string step)
    {
        //Add Project Title
        stepOverview.transform.Find("Top Container").Find("Dept Title").GetComponent<Text>().text = step;
        Debug.Log(stepOverview.name);

        //Add Project Members
        GameObject memContainer = stepOverview.transform.Find("Member Container").gameObject;
        Debug.Log(memContainer.name);


        GameObject memCon_1 = memContainer.transform.Find("member_1").gameObject;
        GameObject memCon_2 = memContainer.transform.Find("member_2").gameObject;
        GameObject memCon_3 = memContainer.transform.Find("member_3").gameObject;

        memCon_1.transform.Find("member - name").GetComponent<Text>().text = "Member 1";
        memCon_2.transform.Find("member - name").GetComponent<Text>().text = "Member 1";
        memCon_3.transform.Find("member - name").GetComponent<Text>().text = "Member 1";


        //Add Project Processes

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
