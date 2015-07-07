using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using iView;

public class EyesScript : MonoBehaviour {

	public bool outro = false;

	float eyesClosedTimepoint = 0;
	float eyesClosedDuration = 0;
	float eyesClosedDurationNeeded;
	float nextHeartbeat = 0.0f;
	int switchHeartbeatSound = 0;

	bool monsterDefeated = false;
	private bool introFinished = false;

	public GameObject monster;
	public MonsterScript monsterscript;
	EyesAnimation eyesAniScript;

	private bool useEyetracking = false;

	// Use this for initialization
	void Start () {
		useEyetracking = GameObject.Find ("EyeTrackingController").GetComponent<GazeInteractions>().useEyeTracking;
		monsterscript = monster.GetComponent<MonsterScript> ();
		eyesAniScript = GameObject.FindGameObjectWithTag("EyesOverlay").GetComponent<EyesAnimation>();
	}
	
	// Update is called once per frame
	void Update () {

		//get the Sample from the Server
		SampleData sample = SMIGazeController.Instance.GetSample();
		
		//get the averaged GazePosition
		Vector3 averageGazePosition = sample.averagedEye.gazePosInUnityScreenCoords ();
		
		//		Debug.Log ("AveragePos (Unity): " + averageGazePosition.x);


		//rechtsklick down
		if (!getEyesClosed() && (Input.GetKeyDown (KeyCode.Mouse1) ||  (averageGazePosition.x == 0 && useEyetracking))) {
			eyesClosedDuration = 0;
			eyesAniScript.CloseEyes();
			eyesClosedTimepoint = Time.time;

			if (monster && monsterscript.monsterFightStarted){
				float distance = monsterscript.distanceToPlayer;
				float timefactor = 0.6f;
				//berechne, wie lange augen geschlossen bleiben muessen
				eyesClosedDurationNeeded = (20f - distance)*timefactor + 2f;
				Debug.Log("Augen sollten " + eyesClosedDurationNeeded + " secs zu sein.");

				heartBeatSoundeffect();

				//deaktiviere endanimation damit man nicht stirbt, waehrend augen zu sind
				monsterscript.playEndAnimation = false;
			}
		}else if (getEyesClosed() && (Input.GetKeyUp (KeyCode.Mouse1) || averageGazePosition.x != 0 && useEyetracking)) {
			//eyesAnimator.SetBool("EyesClosed", false);
			eyesAniScript.OpenEyes();
		}

		//Zeit mit geschlossenen Augen
		if (getEyesClosed ()) {
			eyesClosedDuration = Time.time - eyesClosedTimepoint;

			if (outro && eyesClosedDuration >= eyesClosedDurationNeeded) {
				// play Outro Soundefffect

				if (GameObject.Find ("Relax").GetComponent<AudioSource> ().isPlaying == false) {
					GameObject.Find ("Relax").GetComponent<AudioSource> ().Play ();
				}

			}
		}

	}

	//damit erst alles ausgeloest wird, wenn animation startet, aufgerufen aus animation behaviour
	public void EyesStartToOpen(){

		Debug.Log("Eyes closed for "+eyesClosedDuration);

		//wenn in monsterfight
		if (monster && monsterscript.monsterFightStarted){
			//augen zu kurz geschlossen
			if (eyesClosedDuration < eyesClosedDurationNeeded){
				Debug.Log("closed: "+eyesClosedDuration + " needed: "+eyesClosedDurationNeeded);
				//setze Monster vor das Gesicht
				monsterscript.setCloseup = true;
			} else { //augen lange genug geschlossen
				Debug.Log("closed: "+eyesClosedDuration + " needed: "+eyesClosedDurationNeeded);
				monsterDefeated = true;
				monsterscript.MonsterDefeated();

				if (outro) {
					GameObject.Find ("FPSController").GetComponent<CreditsScript>().startCredits();
				}
			}
		}
		else if (GameObject.FindGameObjectWithTag ("Player").GetComponent<Spiderinteraction>().getTutorialStarted()) {
			Spiderinteraction spiderinteraction = GameObject.FindGameObjectWithTag ("Player").GetComponent<Spiderinteraction> ();
			float EyesClosedMinDuration = spiderinteraction.getEyesClosedMinDuration();
			Debug.Log("closed: "+eyesClosedDuration + " needed: "+EyesClosedMinDuration);
			if (eyesClosedDuration < EyesClosedMinDuration){
				//augen zu kurz geschlossen
				spiderinteraction.setSubtitles("Close your eyes longer and you will calm down.");	
			} else { 
				//augen lange genug geschlossen
				GameObject.FindGameObjectWithTag("Tutorial").SetActive(false);
				spiderinteraction.setSubtitles("Open your Eyes again. See? She's gone! It was only your imagination.");
				spiderinteraction.setTutorialFinished(true);
			}
		}

		eyesClosedDurationNeeded = 0;
		eyesClosedTimepoint = 0;
		eyesClosedDuration = 0;
	}
	
	// return eyesClosed
	public bool getEyesClosed ()
	{
		return eyesAniScript.eyesClosed;
	}

	public void setIntroFinished() {
		Debug.Log ("Geht er hier rein");
		//eyesAnimator.SetBool("EyesClosed", false);
		GameObject.Find ("black").GetComponent<Image>().color = new Color (255, 255, 255, 0);
	}


	public void heartBeatSoundeffect() {

		nextHeartbeat = (Time.time - eyesClosedTimepoint) / eyesClosedDurationNeeded;

		if (GameObject.Find ("Heart Beat A").GetComponent<AudioSource> ().isPlaying == false &&
		    GameObject.Find ("Heart Beat B").GetComponent<AudioSource> ().isPlaying == false) {

				if (switchHeartbeatSound == 0) {
					GameObject.Find ("Heart Beat A").GetComponent<AudioSource> ().Play ();
					switchHeartbeatSound = 1;
				} else {
					GameObject.Find ("Heart Beat B").GetComponent<AudioSource> ().Play ();
					switchHeartbeatSound = 0;
				}
		}

		if (!monsterDefeated) {
			Invoke("heartBeatSoundeffect", nextHeartbeat);
		} 
	}

}