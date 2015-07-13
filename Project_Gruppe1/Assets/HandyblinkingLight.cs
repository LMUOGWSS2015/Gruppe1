using UnityEngine;
using System.Collections;

public class HandyblinkingLight : MonoBehaviour {

	public GameObject handyLight;
	private float lightIntensity;
	private bool blinking = true;

	// Use this for initialization
	void Start () {
		lightIntensity = handyLight. GetComponent<Light>().intensity;
		Invoke ("changeLight", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void stopBlinking() {
		blinking = false;
		handyLight. GetComponent<Light>().intensity = 0f;
	}

	void changeLight() {
		if (handyLight.GetComponent<Light>().intensity == 0f) {
			handyLight. GetComponent<Light>().intensity = lightIntensity;
		} else {
			handyLight. GetComponent<Light>().intensity = 0f;
		}
		if (blinking) {
			Invoke ("changeLight", 1);
		}
	}
}
