using UnityEngine;
using System.Collections;

public class pianoScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (this.gameObject.name == "pianoTriggerStart") {
			if (this.GetComponent<AudioSource>().isPlaying == false) {
				this.GetComponent<AudioSource>().Play();
			}
		} else {
			this.gameObject.SetActive (false);
		}
	}
}
