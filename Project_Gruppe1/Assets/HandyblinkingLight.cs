using UnityEngine;
using System.Collections;

public class HandyblinkingLight : MonoBehaviour {

	public GameObject light;
	private float lightIntensity;
	private bool blinking = true;

	// Use this for initialization
	void Start () {
		lightIntensity = light. GetComponent<Light>().intensity;
		Invoke ("changeLight", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void stopBlinking() {
		blinking = false;
		light. GetComponent<Light>().intensity = 0f;
	}

	void changeLight() {
		if (light.GetComponent<Light>().intensity == 0f) {
			light. GetComponent<Light>().intensity = lightIntensity;
		} else {
			light. GetComponent<Light>().intensity = 0f;
		}
		if (blinking) {
			Invoke ("changeLight", 1);
		}
	}
}
