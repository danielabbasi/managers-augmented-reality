using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickLoadProcess : MonoBehaviour {

	void OnMouseDown(){
		Debug.Log (this.gameObject.name + " Was Clicked.");
		string procName = this.gameObject.name;
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

        //Add Project Members
        GameObject memContainer = procOverview.transform.Find("Member Container").gameObject;
        Debug.Log(memContainer.name);


        memContainer.transform.Find("member_1").GetComponent<Text>().text = "Team Member 1";
        memContainer.transform.Find("member_2").GetComponent<Text>().text = "Team Member 2";
        memContainer.transform.Find("member_3").GetComponent<Text>().text = "Team Member 3";


        //Add Project Processes
        GameObject midContainer = procOverview.transform.Find("Middle Container").gameObject;

        GameObject stepRow_1 = midContainer.transform.Find("Step_1").gameObject;
        stepRow_1.transform.Find("title-name").GetComponent<Text>().text = "Step 1";
        stepRow_1.transform.Find("title-die").GetComponent<Text>().text = "5";
        stepRow_1.transform.Find("title-status").GetComponent<Image>().color = new Color32(240, 0, 0, 255);
        stepRow_1.transform.Find("title-report").GetComponent<Text>().text = "Step 1";

        GameObject stepRow_2 = midContainer.transform.Find("Step_2").gameObject;
        stepRow_2.transform.Find("title-name").GetComponent<Text>().text = "Step 2";
        stepRow_2.transform.Find("title-die").GetComponent<Text>().text = "1";
        stepRow_2.transform.Find("title-status").GetComponent<Image>().color = new Color32(220, 150, 0, 255);
        stepRow_2.transform.Find("title-report").GetComponent<Text>().text = "Step 2";
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
