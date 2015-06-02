using UnityEngine;
using System.Collections;

public class EyesScript : MonoBehaviour {

	private bool eyesClosed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			GameObject.FindGameObjectWithTag("EyesOverlay").GetComponent<Animator>().SetTrigger("CloseEyes");
			eyesClosed = true;
		}
		if (Input.GetKeyUp (KeyCode.Mouse1)) {
			GameObject.FindGameObjectWithTag("EyesOverlay").GetComponent<Animator>().SetTrigger("OpenEyes");
			eyesClosed = false;
		}

	}

	// return eyesClosed
	public bool getEyesClosed ()
	{
		return eyesClosed;
	}
}