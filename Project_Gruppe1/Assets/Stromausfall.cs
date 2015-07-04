using UnityEngine;
using System.Collections;

public class Stromausfall : MonoBehaviour {

	private GameObject[] lightsUpstairs;

	private bool playOnce = true;

	void OnTriggerEnter(Collider other) {
		if (this.GetComponent<AudioSource>().isPlaying == false) {
			this.GetComponent<AudioSource>().Play ();
		}

		lightsUpstairs = GameObject.FindGameObjectsWithTag("lightsUpstairs");	
		Invoke("turnLightsOff", 0.7f);
	}

	private void turnLightsOff() {
		foreach (GameObject light in lightsUpstairs) {
			light.SetActive(false);		
		}
	}
}
