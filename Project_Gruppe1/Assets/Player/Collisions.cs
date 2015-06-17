using UnityEngine;
using System.Collections;

public class Collisions : MonoBehaviour {
	
	// Monster: Script for closing eyes
	private EyesScript es;

	// Hide spot
	private bool hideSpotOpen;
	private GameObject go;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		Debug.Log ("#################Start Collisions Script");
		
		// Monster: Get eye script
		es  = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EyesScript>();

		// Hide spot
		hideSpotOpen = false;
		go = GameObject.Find("/Grundriss/Erdgeschoss/Versteck/hide_spot_2/door");
		rb = go.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		rb.useGravity = false;

	}
	
	// Update is called once per frame
	void Update () {

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
			if (!hideSpotOpen) {
				OpenHideSpot();
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
		hideSpotOpen = true;
		go.transform.rotation = Quaternion.Euler(0, 0, -0.5f);
		rb.isKinematic = false;
		rb.useGravity = true;
	}
}
