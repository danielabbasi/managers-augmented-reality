using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickLoadSteps : MonoBehaviour {

	void OnMouseDown(){
		Debug.Log (this.gameObject.name + " Was Clicked.");
		string proName = this.gameObject.name;
		GameObject dept = this.gameObject.transform.parent.parent.parent.gameObject;
		GameObject processOverview = dept.transform.Find ("ProcessOverview").gameObject;
		GameObject stepOverview = dept.transform.Find("StepOverview").gameObject;

		setCanvasGroup (0f, processOverview);
		setCanvasGroup (1f, stepOverview);

		setToView (stepOverview);
		setToHidden(processOverview);
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
