using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour {


	void OnMouseDown(){
		GameObject currentView = this.transform.parent.parent.gameObject;
        GameObject newView = new GameObject();
		switch (currentView.name)
		{
		case "DeptOverview":
			    Debug.Log("Go to menu");
                newView = GameObject.Find("Menu");
                break;
		case "ProjOverview":
                newView = currentView.transform.parent.Find("DeptOverview").gameObject;
			break;
        case "ProcessOverview":
                newView = currentView.transform.parent.Find("ProjOverview").gameObject;
                break;
		case "StepOverview":
                newView = currentView.transform.parent.Find("ProcessOverview").gameObject;
                break;
		case "MemberContact":
                newView = currentView.transform.parent.Find("StepOverview").gameObject;
                break;
		default:
                newView = currentView.transform.parent.Find("StepOverview").gameObject;
                break;
		}
        Debug.Log(newView.name);
        if (newView.name == "StepOverview")
        {
            setCanvasGroup(0, currentView);
            setToHidden(currentView);
        }
        else
        {
            setCanvasGroup(0, currentView);
            setCanvasGroup(1, newView);
            setToView(newView);
            setToHidden(currentView);
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
