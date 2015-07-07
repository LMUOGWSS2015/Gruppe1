using UnityEngine;
using System.Collections;

public class CloseDoorEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().open == true) {
			GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().ChangeDoorState (true);
		}

		GameObject.Find ("FirstPersonCharacter").GetComponent<EyesScript> ().outro = true;

	}
}
