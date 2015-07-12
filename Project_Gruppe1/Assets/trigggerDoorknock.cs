using UnityEngine;
using System.Collections;

public class trigggerDoorknock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Invoke ("hitAgainstDoor", 0.1f);
		Invoke ("hitAgainstDoor", 1.1f);
		Invoke ("hitAgainstDoor", 1.8f);
	}

	public void hitAgainstDoor(){
		GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().hitAgainstDoor ();
	}
}
