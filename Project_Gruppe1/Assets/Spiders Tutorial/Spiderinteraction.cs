using UnityEngine;
using System.Collections;

/*
 * Script for FPSController
 */
public class SpiderInteraction : MonoBehaviour {

	private EyesScript es;

	// Use this for initialization
	void Start () 
	{
		es  = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EyesScript>();
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter(Collider other) 
	{
		// Game object need tag "Monster"
		if (other.gameObject.CompareTag ("Spiders")) 
		{
			Debug.Log ("Ahhhh Spider!");
			StartCoroutine(SpiderDie(3));
		}
	}

	IEnumerator SpiderDie(int x) {
		Debug.Log ("Oh nein, ich sterbe... In " + x + " Sekunden bin ich tot, wenn ich nicht die Augen schliesse.");
		yield return new WaitForSeconds(x);
		if (!es.getEyesClosed ()) {
			GameObject.FindGameObjectWithTag ("Spiders").GetComponent<Animator> ().SetTrigger ("SpiderMove");
			//TODO SpiderMove Animation
		} else {
			GameObject.FindGameObjectWithTag("Spiders").SetActive(false);
			Debug.Log ("============================== Gefahr vorüber! Augen können geöffnet werden.");
		}
	}
}
