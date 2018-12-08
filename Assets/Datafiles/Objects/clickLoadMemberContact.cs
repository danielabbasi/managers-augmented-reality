using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickLoadMemberContact : MonoBehaviour {

	void OnMouseDown(){
		Debug.Log (this.gameObject.name + " Was Clicked.");
		string memberName = this.gameObject.name;
		GameObject dept = this.gameObject.transform.parent.parent.parent.gameObject;
		GameObject memberContact = dept.transform.Find ("MemberContact").gameObject;

        setNewViewData(memberContact,memberName);

        setCanvasGroup (1f, memberContact);
		setToView (memberContact);
	}

    void setNewViewData(GameObject memberContact, string member)
    {
        //Add Project Title
        memberContact.transform.Find("Top Container").Find("Dept Title").GetComponent<Text>().text = member;
        Debug.Log(memberContact.name);

        //Add Project Members
        GameObject memContainer = memberContact.transform.Find("Member Info").gameObject;
        Debug.Log(memContainer.name);



       


        //Add Project Processes

    }

    void setCanvasGroup(float alpha, GameObject o){
		CanvasGroup canvas = o.GetComponent ("CanvasGroup") as CanvasGroup;
		canvas.alpha = alpha;
	}

	void setToView(GameObject o){
		o.transform.localPosition = new Vector3(-0.8f,0,0);
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
