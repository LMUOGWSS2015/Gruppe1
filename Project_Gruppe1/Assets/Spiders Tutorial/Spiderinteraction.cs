using UnityEngine;
using System.Collections;

/*
 * Script for FPSController
 */
public class Spiderinteraction : MonoBehaviour {

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
		// Game object need tag "Spiders"
		if (other.gameObject.CompareTag ("SpiderCollider")) 
		{
			Debug.Log ("Ahhhh Spider!");
			StartCoroutine(Spiderattack(3));
			Debug.Log ("Die Spinne existiert nur in meiner Fantasie. Ich schließe lieber 3 sec meine Augen");
		}
	}

	IEnumerator Spiderattack(int x) {
			yield return new WaitForSeconds(x);
		if (!es.getEyesClosed ()) {
			Debug.Log ("Zu spät!");			
			//TODO SpiderMove Animation
			//GameObject.FindGameObjectWithTag ("Spiders").GetComponent<Animator> ().SetTrigger ("SpiderMove");			
			} else {
				GameObject.FindGameObjectWithTag("Spiders").SetActive(false);
				Debug.Log (" Gefahr vorüber! Augen können geöffnet werden.");
			}
		}
}

