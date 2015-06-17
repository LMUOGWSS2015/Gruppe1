using UnityEngine;
using System.Collections;

public class Collisions : MonoBehaviour {
	
	// Monster: Script for closing eyes
	private EyesScript es;

	// Hide spot
	private bool openHideSpot;
	private GameObject go;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		
		// Monster: Get eye script
		es  = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EyesScript>();

		// Hide spot
		openHideSpot = false;
		go = GameObject.Find("/Grundriss/Erdgeschoss/Versteck/hide_spot_2/Tuer/door");
//		rb = go.GetComponent<Rigidbody>();
//		rb.isKinematic = true;
//		rb.useGravity = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (openHideSpot) {
			Quaternion targetRotation = Quaternion.Euler (0, 91, 0);
			go.transform.localRotation = Quaternion.Lerp (go.transform.localRotation, targetRotation, 2.0f * Time.deltaTime);
		}
	}

	// Trigger enter: check tag of entered (collided) game object
	void OnTriggerEnter(Collider other) {

		// Monster
		if (other.gameObject.CompareTag ("Monster")) {
			Debug.Log ("Ahhhh Monster!");
			StartCoroutine(Die(3));
		}

		// Open hide spot
		if (other.gameObject.CompareTag ("HideSpot")) {
			if (!openHideSpot) {
				openHideSpot = true;
			}
		}
	}

	/*
	 * Wait action (seconds)
	 */
	IEnumerator Die(int x) {
		Debug.Log ("Oh nein, ich sterbe... In " + x + " Sekunden bin ich tot, wenn ich nicht die Augen schliesse.");
		yield return new WaitForSeconds(x);
		if (!es.getEyesClosed ()) {
			GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Animator>().SetTrigger("die");
		}
		Debug.Log ("============================== Gefahr vorüber! Augen können geöffnet werden.");
	}

	/*
	 * Open hide spot
	 */
	void OpenHideSpot() {
		openHideSpot = true;
//		go.transform.rotation = Quaternion.Euler(0, 0, -0.5f);
//		rb.isKinematic = false;
//		rb.useGravity = true;
	}
}
