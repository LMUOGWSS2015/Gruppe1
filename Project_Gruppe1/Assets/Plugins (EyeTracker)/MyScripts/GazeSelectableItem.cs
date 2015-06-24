using UnityEngine;
using System.Collections;

public class GazeSelectableItem : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnGazeEnter() {
		Debug.Log ("++++++++++++++++++++++++++++++++ OnGazeEnter()");
		GetComponent<Renderer>().material.color = Color.blue;
	}
	
	public void OnGazeExit() {
		Debug.Log ("------------------------------------------------------------------------ OnGazeExit()");
		GetComponent<Renderer>().material.color = Color.black;
		CancelInvoke("WaitForMissingsample");
		StartCoroutine(WaitForMissingsample());
	}
	
	IEnumerator WaitForMissingsample() {
		yield return new WaitForSeconds(0.25f);
		//		scaleDestination = startScale;
		
	}
}
