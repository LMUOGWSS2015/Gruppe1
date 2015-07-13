using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using iView;

public class EyesScript : MonoBehaviour {

	public bool outro = false;
	public bool stopHeartBeat = false;

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

	Spiderinteraction spiderinteraction;

	private bool useEyetracking = false;

	bool playEndDialog = true;

	// Use this for initialization
	void Start () {
		useEyetracking = GazeInteractions.useEyeTracking;
		monsterscript = monster.GetComponent<MonsterScript> ();
		eyesAniScript = GameObject.FindGameObjectWithTag("EyesOverlay").GetComponent<EyesAnimation>();
		stopHeartBeat = false;
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

			if (GameObject.Find("FPSController").GetComponent<Spiderinteraction>().tutorialStarted == true) {
				spiderinteraction = GameObject.FindGameObjectWithTag ("Player").GetComponent<Spiderinteraction> ();
				eyesClosedDurationNeeded = spiderinteraction.getEyesClosedMinDuration();
				GameObject.Find ("Subtitle").GetComponent<subtitlesScript>().fadeOutText();
			}

			if (monster && monsterscript.monsterFightStarted){
				float distance = monsterscript.distanceToPlayer;
				float timefactor = 0.6f;
				//berechne, wie lange augen geschlossen bleiben muessen
				eyesClosedDurationNeeded = (20f - distance)*timefactor + 2f;
				Debug.Log("Augen sollten " + eyesClosedDurationNeeded + " secs zu sein.");

				//deaktiviere endanimation damit man nicht stirbt, waehrend augen zu sind
				monsterscript.playEndAnimation = false;
			}
		} else if (getEyesClosed() && (Input.GetKeyUp (KeyCode.Mouse1) || averageGazePosition.x != 0 && useEyetracking)) {
			//eyesAnimator.SetBool("EyesClosed", false);
			eyesAniScript.OpenEyes();
		}

		//Zeit mit geschlossenen Augen
		if (getEyesClosed ()) {
			eyesClosedDuration = Time.time - eyesClosedTimepoint;

			if (outro && eyesClosedDuration >= eyesClosedDurationNeeded && monster && monsterscript.monsterFightStarted) {
				// play Outro Soundefffect
				GameObject.Find ("Heart Beat A").GetComponent<AudioSource> ().volume = 0.0f;
				GameObject.Find ("Heart Beat B").GetComponent<AudioSource> ().volume = 0.0f;

				if (GameObject.Find ("Relax").GetComponent<AudioSource> ().isPlaying == false && playEndDialog) {
					Debug.Log("Jetzt spielt er Soundeffekt ende");
					GameObject.Find ("Relax").GetComponent<AudioSource> ().Play ();
					playEndDialog = false;
				}

			}
			if (eyesClosedDuration >= eyesClosedDurationNeeded) {
				GameObject.Find ("MonsterFeetSound").GetComponent<AudioSource> ().loop = false;
			}
		}

	}

	//damit erst alles ausgeloest wird, wenn animation startet, aufgerufen aus animation behaviour
	public void EyesStartToOpen(){

	//	Debug.Log("Eyes closed for "+eyesClosedDuration);
		//stopHeartBeat = true;

		//wenn in monsterfight
		if (monster && monsterscript.monsterFightStarted){
			//augen zu kurz geschlossen
			if (eyesClosedDuration < eyesClosedDurationNeeded){
			//	Debug.Log("closed: "+eyesClosedDuration + " needed: "+eyesClosedDurationNeeded);
				//setze Monster vor das Gesicht
				if (eyesClosedDuration >= 0.7f) {
					monsterscript.setCloseup = true;
				}
			} else { //augen lange genug geschlossen
				//Debug.Log("closed: "+eyesClosedDuration + " needed: "+eyesClosedDurationNeeded);
				monsterDefeated = true;
				monsterscript.MonsterDefeated();

				if (outro) {
					GameObject.Find ("FPSController").GetComponent<CreditsScript>().startCredits();
				}
				stopHeartBeat = true;

			}
		}
		else if (GameObject.FindGameObjectWithTag ("Player").GetComponent<Spiderinteraction>().getTutorialStarted()) {
				
			Debug.Log("closed: "+eyesClosedDuration + " needed: " + eyesClosedDurationNeeded);
			if (eyesClosedDuration < eyesClosedDurationNeeded){
				//augen zu kurz geschlossen
				spiderinteraction.setSubtitles("Close your eyes longer and you will calm down.");	

			} else { 
				//augen lange genug geschlossen
				GameObject.FindGameObjectWithTag("Tutorial").SetActive(false);
				spiderinteraction.setSubtitles("It was only your imagination.");
				spiderinteraction.setTutorialFinished(true);
				stopHeartBeat = true;

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
		//Debug.Log ("Geht er hier rein");
		//eyesAnimator.SetBool("EyesClosed", false);
		GameObject.Find ("black").GetComponent<Image>().color = new Color (255, 255, 255, 0);
	}


	public void heartBeatSoundeffect() {
		if (getEyesClosed ()) {
			nextHeartbeat = Mathf.Clamp (((Time.time - eyesClosedTimepoint) / eyesClosedDurationNeeded), 0.01f, 1.0f);
		} else if(GameObject.Find("FPSController").GetComponent<Spiderinteraction>().tutorialStarted == true) {
			nextHeartbeat = 0.2f;
		} else {
			Debug.Log ("So weit ist monster weg: " + monsterscript.distanceToPlayer);
			nextHeartbeat = monsterscript.distanceToPlayer/12.0f;
		}


		if (GameObject.Find ("Heart Beat A").GetComponent<AudioSource> ().isPlaying == false &&
		    GameObject.Find ("Heart Beat B").GetComponent<AudioSource> ().isPlaying == false) {

				if (switchHeartbeatSound == 0) {
					GameObject.Find ("Heart Beat A").GetComponent<AudioSource> ().Play ();
					switchHeartbeatSound = 1;
				} else {
					GameObject.Find ("Heart Beat B").GetComponent<AudioSource> ().Play ();
					nextHeartbeat = nextHeartbeat/2;
					switchHeartbeatSound = 0;
				}
		}
		Debug.Log ("stop hear beat: " + stopHeartBeat); 

		if (!stopHeartBeat) {
			Invoke ("heartBeatSoundeffect", nextHeartbeat);
		} else {
			switchHeartbeatSound = 0;
		}

	}

}