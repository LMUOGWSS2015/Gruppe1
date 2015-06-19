using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EyesScript : MonoBehaviour {

	Animator eyesAnimator;
	float eyesClosedTimepoint = 0;
	float eyesClosedDuration = 0;
	float eyesClosedDurationNeeded = 0;
	private bool introFinished = false;

	public GameObject monster;
	public MonsterScript monsterscript;

	// Use this for initialization
	void Start () {
		eyesAnimator = GameObject.FindGameObjectWithTag("EyesOverlay").GetComponent<Animator>();
		eyesAnimator.SetBool("EyesClosed", true);
		monsterscript = monster.GetComponent<MonsterScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		//rechtsklick down
		if (Input.GetKeyDown (KeyCode.Mouse1) && !getEyesClosed()) {
			eyesAnimator.SetBool("EyesClosed", true);
			eyesClosedTimepoint = Time.time;

			if (monster && monsterscript.monsterFightStarted){
				float distance = monsterscript.distanceToPlayer;
				float timefactor = 0.6f;
				//berechne, wie lange augen geschlossen bleiben muessen
				eyesClosedDurationNeeded = (20f - distance)*timefactor + 2f;
				Debug.Log("Augen sollten " + eyesClosedDurationNeeded + " secs zu sein.");

				//deaktiviere endanimation damit man nicht stirbt, waehrend augen zu sind
				monsterscript.playEndAnimation = false;
			}
		}
		//rechtsklick up
		if (Input.GetKeyUp (KeyCode.Mouse1 ) && getEyesClosed()) {
			eyesAnimator.SetBool("EyesClosed", false);
		}

		//Zeit mit geschlossenen Augen
		if (getEyesClosed()) {
			eyesClosedDuration = Time.time - eyesClosedTimepoint;
		}

	}

	//damit erst alles ausgeloest wird, wenn animation startet
	public void EyesStartToOpen(){
		//wenn in monsterfight
		if (monster && monsterscript.monsterFightStarted){
			//augen zu kurz geschlossen
			if (eyesClosedDuration < eyesClosedDurationNeeded){
				Debug.Log("closed: "+eyesClosedDuration + " needed: "+eyesClosedDurationNeeded);
				//setze Monster vor das Gesicht
				monsterscript.setCloseup = true;
			} else { //augen lange genug geschlossen
				Debug.Log("closed: "+eyesClosedDuration + " needed: "+eyesClosedDurationNeeded);
				monsterscript.MonsterDefeated();
			}
		}
		
		eyesClosedDuration = 0;
	}
	
	// return eyesClosed
	public bool getEyesClosed ()
	{
		return eyesAnimator.GetBool("EyesClosed");
	}

	public void setIntroFinished() {
		eyesAnimator.SetBool("EyesClosed", false);
		GameObject.Find ("black").GetComponent<Image> ().color = new Color (255, 255, 255, 0);
	}
}