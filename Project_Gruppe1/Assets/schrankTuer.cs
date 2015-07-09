using UnityEngine;
using System.Collections;

public class schrankTuer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<DoorOpenScript> ().open == true) {
			this.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
		}
	}
}
