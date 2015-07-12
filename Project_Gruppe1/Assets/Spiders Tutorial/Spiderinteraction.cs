using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

/*
 * Script for FPSController
 */
public class Spiderinteraction : MonoBehaviour {

	private EyesScript es;
	private GameObject player;
	private Text subtitles;
	private float EyesClosedMinDuration = 5.0f;
	public bool tutorialStarted = false;
	public bool tutorialFinished = false;

	// Use this for initialization
	void Start () 
	{
		es  = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EyesScript>();
		player = GameObject.FindGameObjectWithTag ("Player");
		subtitles = GameObject.FindGameObjectWithTag ("Subtitles").GetComponent<Text>();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("TutorialCollider") && !tutorialFinished) {
			GameObject.FindGameObjectWithTag ("TutorialCollider").SetActive (false);
			player.GetComponent<FirstPersonController> ().enabled = false;
			player.GetComponent<CharacterController> ().enabled = false;

			es.GetComponent<SmoothLookAt> ().target = GameObject.FindGameObjectWithTag ("Spider").GetComponent<Transform> ();
			es.GetComponent<SmoothLookAt> ().enabled = true;

			tutorialStarted = true;
			subtitles.text = "See the giant spider? Close your Eyes.";

			if (GameObject.Find ("EyeTrackingController").GetComponent<GazeInteractions> ().useEyeTracking == false) {
				subtitles.text += "\n [Press Right Mouse Button]";
			}

			GameObject.FindGameObjectWithTag ("Tutorial").GetComponent<Animator> ().SetTrigger ("TutorialTrigger");
		}
	}

	void Update(){
		if (tutorialStarted && tutorialFinished) {/*
			player.GetComponent<FirstPersonController> ().m_MouseLook.ResetRotation (
				player.GetComponentInChildren<FirstPersonController>().transform,
				player.GetComponentInChildren<FirstPersonController>().m_Camera.transform
			);*/
			es.GetComponent<SmoothLookAt>().enabled = false;
			player.GetComponent<FirstPersonController>().enabled = true;
			player.GetComponent<CharacterController>().enabled = true;
			StartCoroutine(eraseSubtitlesAfterSecs(3));
			tutorialStarted = false;
		}
	}


	IEnumerator eraseSubtitlesAfterSecs(int x) {
		yield return new WaitForSeconds (x);
		//TODO fadeOut
		subtitles.text = "";
	}

	public float getEyesClosedMinDuration(){
			return EyesClosedMinDuration;
	}

	public bool getTutorialStarted(){
		return tutorialStarted;
	}

	public void setSubtitles(string t){
		eraseSubtitlesAfterSecs (1);
		//TODO fadeIn
		subtitles.text = t;
	}

	public void setTutorialFinished(bool b){
		tutorialFinished = b;
	}
}

