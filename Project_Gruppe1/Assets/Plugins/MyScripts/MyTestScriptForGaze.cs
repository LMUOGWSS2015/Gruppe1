using UnityEngine;
using System.Collections;
using iView;

public class MyTestScriptForGaze : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("MyTestScriptForGaze START");
	}
	
	// Update is called once per frame
	void Update () {
		//get the Sample from the Server
		SampleData sample = SMIGazeController.Instance.GetSample();
		
		//get the averaged GazePosition
		Vector3 averageGazePosition = sample.averagedEye.gazePosInUnityScreenCoords (); 

	}
}
