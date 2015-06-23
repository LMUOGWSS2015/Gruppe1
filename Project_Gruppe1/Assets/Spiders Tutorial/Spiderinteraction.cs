using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

/*
 * Script for FPSController
 */
public class Spiderinteraction : MonoBehaviour {

	private EyesScript es;
	private string subtitle;
	private GameObject player;
	private Text subtitles;

	// Use this for initialization
	void Start () 
	{
		es  = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EyesScript>();
		player = GameObject.FindGameObjectWithTag ("Player");
		subtitles = GameObject.FindGameObjectWithTag ("Subtitles").GetComponent<Text>();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("TutorialCollider")) 
		{
			GameObject.FindGameObjectWithTag ("TutorialCollider").SetActive(false);
			player.GetComponent<FirstPersonController>().enabled = false;
			player.GetComponent<CharacterController>().enabled = false;

			es.GetComponent<SmoothLookAt>().target = GameObject.FindGameObjectWithTag("Spider").GetComponent<Transform>();
			es.GetComponent<SmoothLookAt> ().enabled = true;

			subtitles.text = "See the giant spider? Close your Eyes!";
			GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Animator>().SetTrigger("TutorialTrigger");
			StartCoroutine(Spiderattack(10));
		}
	}

	IEnumerator Spiderattack(int x) {
		yield return new WaitForSeconds(x);
		if (!es.getEyesClosed ()) {
			subtitles.text = "You took too long.";	
			//TODO what else happens here?
		} else {
			GameObject.FindGameObjectWithTag("Tutorial").SetActive(false);
			subtitles.text = "Open your Eyes again. See? It was only your imagination.";	
		}
		yield return new WaitForSeconds(2);
		player.GetComponent<FirstPersonController> ().m_MouseLook.ResetRotation (
			player.GetComponentInChildren<FirstPersonController>().transform,
			player.GetComponentInChildren<FirstPersonController>().m_Camera.transform
		);
		es.GetComponent<SmoothLookAt>().enabled = false;
		player.GetComponent<FirstPersonController>().enabled = true;
		player.GetComponent<CharacterController>().enabled = true;
		yield return new WaitForSeconds(5);
		subtitles.text = "";	
	}
}

