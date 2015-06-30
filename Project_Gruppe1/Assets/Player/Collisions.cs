using UnityEngine;
using System.Collections;

public class Collisions : MonoBehaviour {
	
	// Monster: Script for closing eyes
	private EyesScript es;


	// Use this for initialization
	void Start () {		
		// Monster: Get eye script
		es  = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EyesScript>();
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
}
