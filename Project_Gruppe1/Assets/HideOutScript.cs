using UnityEngine;
using System.Collections;

public class HideOutScript : MonoBehaviour {

	// Hide spot
	private bool openHideSpot;
	private GameObject go;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {


		// Hide spot
		openHideSpot = false;
		go = GameObject.Find("hideOutDoor");

				//		rb = go.GetComponent<Rigidbody>();
		//		rb.isKinematic = true;
		//		rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (openHideSpot) {
			if (GameObject.Find ("TriggerHideOut").GetComponent<BoxCollider>().enabled == true) {
				GameObject.Find ("TriggerHideOut").GetComponent<BoxCollider>().enabled = false;
			}
			Quaternion targetRotation = Quaternion.Euler (0, 91, 0);
			go.transform.localRotation = Quaternion.Lerp (go.transform.localRotation, targetRotation, 2.0f * Time.deltaTime);	


		}
	}

	public void OpenHideSpot() {
		if (!openHideSpot) {

			if (this.GetComponent<AudioSource>().isPlaying == false) {
				this.GetComponent<AudioSource>().Play();
			}

			openHideSpot = true;
		}

	}
}
