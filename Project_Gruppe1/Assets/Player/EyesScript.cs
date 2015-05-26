using UnityEngine;
using System.Collections;

public class EyesScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			GameObject.FindGameObjectWithTag("EyesOverlay").GetComponent<Animator>().SetTrigger("CloseEyes");
		}
		if (Input.GetKeyUp (KeyCode.Mouse1)) {
			GameObject.FindGameObjectWithTag("EyesOverlay").GetComponent<Animator>().SetTrigger("OpenEyes");
		}
	}
}
