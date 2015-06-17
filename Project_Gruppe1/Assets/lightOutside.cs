using UnityEngine;
using System.Collections;

public class lightOutside : MonoBehaviour {
	private Light lichtDraussen;
	private float lichtWert;


	// Use this for initialization
	void Start () {
		lichtDraussen = this.gameObject.GetComponent<Light> ();
		lichtWert = lichtDraussen.intensity;
		lichtDraussen.intensity = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().open == true) {
			lichtDraussen.intensity = lichtWert;
		}
	}
}
