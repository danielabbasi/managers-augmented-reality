using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopDeptOverview : MonoBehaviour {

	public Font font;	

	// Use this for initialization
	void Start () {

		//
		GameObject dept = GameObject.Find("Marketing");
		GameObject deptOverview = dept.transform.Find("DeptOverview").gameObject;
		GameObject topContainer = deptOverview.transform.Find("Top Container").gameObject;
		GameObject title = topContainer.transform.Find("Dept Title").gameObject;
		//
		title.transform.SetParent(topContainer.transform);
		//
		Text myText = title.AddComponent<Text>();
		myText.font = font;
		myText.text = "Title Test";
		myText.alignment = TextAnchor.MiddleCenter;
	}

	// Update is called once per frame
	void Update () {

	}
}
