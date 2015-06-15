using UnityEngine;
using System.Collections;

public class Stromausfall : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		GameObject[] lightsUpstairs;

		lightsUpstairs = GameObject.FindGameObjectsWithTag("lightsUpstairs");	

		foreach (GameObject light in lightsUpstairs) {
			light.SetActive(false);		
		}

		// noch Soundeffekt rein!!!
	}
}
