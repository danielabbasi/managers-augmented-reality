using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CreateCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject title = GameObject.Find("Title");
		title.transform.SetParent(this.transform);

		Text myText = title.AddComponent<Text>();
		myText.text = "Title Test";
	}
	
	// Update is called once per frame
	void Update () {

	}
}
