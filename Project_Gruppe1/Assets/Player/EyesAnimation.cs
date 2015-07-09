using UnityEngine;
using System.Collections;

public class EyesAnimation : MonoBehaviour {

	public bool eyesClosed = false;
	bool closeEyesAnimation = false;
	bool openEyesAnimation = false;
	GameObject lidOben, lidUnten;
	float closeSpeed = 3f, openSpeed = 2f;
	Vector2 closedGoal, obenOpenGoal, untenOpenGoal;

	// Use this for initialization
	void Start () {
		lidOben = GameObject.Find("LidOben");
		lidUnten = GameObject.Find("LidUnten");

		closedGoal = new Vector2 (0, 0);
		obenOpenGoal = new Vector2 (0, Screen.height);
		untenOpenGoal = new Vector2 (0, -Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		if (closeEyesAnimation) {
			lidOben.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(lidOben.GetComponent<RectTransform>().anchoredPosition, closedGoal, closeSpeed * Time.deltaTime);
			lidUnten.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(lidUnten.GetComponent<RectTransform>().anchoredPosition, closedGoal, closeSpeed * Time.deltaTime);
		} else if (openEyesAnimation){
			lidOben.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(lidOben.GetComponent<RectTransform>().anchoredPosition, obenOpenGoal, closeSpeed * Time.deltaTime);
			lidUnten.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(lidUnten.GetComponent<RectTransform>().anchoredPosition, untenOpenGoal, closeSpeed * Time.deltaTime);
		}
	}

	public void CloseEyes(){
		//Debug.Log("CloseEyes");
		closeEyesAnimation = true;
		openEyesAnimation = false;
		eyesClosed = true;
	}

	public void OpenEyes(){
		//Debug.Log("OpenEyes");
		eyesClosed = false;
		openEyesAnimation = true;
		closeEyesAnimation = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<EyesScript> ().EyesStartToOpen ();
	}
}
