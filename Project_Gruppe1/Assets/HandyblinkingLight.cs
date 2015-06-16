using UnityEngine;
using System.Collections;

public class HandyblinkingLight : MonoBehaviour {

	public GameObject light;
	private float lightIntensity;

	// Use this for initialization
	void Start () {
		lightIntensity = light. GetComponent<Light>().intensity;
		Invoke ("changeLight", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void changeLight() {
		if (light.GetComponent<Light>().intensity == 0f) {
			light. GetComponent<Light>().intensity = lightIntensity;
		} else {
			light. GetComponent<Light>().intensity = 0f;
		}

		Invoke ("changeLight", 1);
	}
}
