using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

	void OnTriggerEnter(Collider other) 
	{
		// Game object need tag "Spiders"
		if (other.gameObject.CompareTag ("SpiderCollider")) 
		{
			GameObject.FindGameObjectWithTag ("SpiderCollider").SetActive (false);
			GameObject.FindGameObjectWithTag("Subtitles").GetComponent<Text>().text = "Close your Eyes";
			StartCoroutine(Spiderattack(3));
		}
	}

	IEnumerator Spiderattack(int x) {
		yield return new WaitForSeconds(x);
		if (!es.getEyesClosed ()) {
			GameObject.FindGameObjectWithTag("Subtitles").GetComponent<Text>().text = "You took too long.";		
			} else {
				GameObject.FindGameObjectWithTag("Spiders").SetActive(false);
				GameObject.FindGameObjectWithTag("Subtitles").GetComponent<Text>().text = "See? It was only your imagination.";	
			}
		yield return new WaitForSeconds(5);
		GameObject.FindGameObjectWithTag("Subtitles").GetComponent<Text>().text = "";	
		}
}

