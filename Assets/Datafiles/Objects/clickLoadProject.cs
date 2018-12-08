using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class clickLoadProject : MonoBehaviour {

	void OnMouseDown(){
		Debug.Log (this.gameObject.name + " Was Clicked.");
		GameObject projRow= this.gameObject;
        string projName = projRow.transform.Find("project-name").GetComponent<Text>().text;
        Debug.Log(projName);
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
        Debug.Log(projOverview.name);
        //Add Project Members
        GameObject memContainer = projOverview.transform.Find("Member Container").gameObject;
        Debug.Log(memContainer.name);


        memContainer.transform.Find("member_1").GetComponent<Text>().text ="Team Member 1";
        memContainer.transform.Find("member_2").GetComponent<Text>().text = "Team Member 2";
        memContainer.transform.Find("member_3").GetComponent<Text>().text = "Team Member 3";


        //Add Project Processes
        GameObject midContainer = projOverview.transform.Find("Middle Container").gameObject;

        GameObject procRow_1 = midContainer.transform.Find("Process_1").gameObject;
        procRow_1.transform.Find("title-name").GetComponent<Text>().text = "Process 1";
        procRow_1.transform.Find("title-sie").GetComponent<Text>().text = "5/10";
        procRow_1.transform.Find("title-status").GetComponent<Image>().color = new Color32(240, 0, 0, 255);
        procRow_1.transform.Find("title-report").GetComponent<Text>().text = "Process 1";

        GameObject procRow_2 = midContainer.transform.Find("Process_2").gameObject;
        procRow_2.transform.Find("title-name").GetComponent<Text>().text = "Process 2";
        procRow_2.transform.Find("title-sie").GetComponent<Text>().text = "1/10";
        procRow_2.transform.Find("title-status").GetComponent<Image>().color = new Color32(220,150,0,255);
        procRow_2.transform.Find("title-report").GetComponent<Text>().text = "Process 2";



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
