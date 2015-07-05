using UnityEngine;
using System.Collections;

public class ScriptEndScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().open == true) {

		}



	}


}
