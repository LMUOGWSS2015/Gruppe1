using UnityEngine;
using System.Collections;

public class EyesScript : MonoBehaviour {

	Animator eyesAnimator;
	float eyesClosedTimepoint = 0;
	float eyesClosedDuration = 0;
	float eyesClosedDurationNeeded = 0;
	GameObject monster;
	bool monsterFightStarted = false;

	// Use this for initialization
	void Start () {
		eyesAnimator = GameObject.FindGameObjectWithTag("EyesOverlay").GetComponent<Animator>();
		monster = GameObject.FindGameObjectWithTag ("Monster");
	}
	
	// Update is called once per frame
	void Update () {
		//rechtsklick down
		if (Input.GetKeyDown (KeyCode.Mouse1) && !getEyesClosed()) {
			eyesAnimator.SetBool("EyesClosed", true);
			eyesClosedTimepoint = Time.time;

			if (monster.GetComponent<MonsterScript>().monsterFightStarted){
				float distance = monster.GetComponent<MonsterScript>().distanceToPlayer;
				float timefactor = 0.6f;
				eyesClosedDurationNeeded = (20f - distance)*timefactor + 3f;
				Debug.Log("Augen sollten " + eyesClosedDurationNeeded + " secs zu sein.");
				monster.GetComponent<MonsterScript>().eyesClosedForFight = true;
			}
		}
		//rechtsklick up
		if (Input.GetKeyUp (KeyCode.Mouse1 )&& getEyesClosed()) {
			eyesAnimator.SetBool("EyesClosed", false);

			if (monster.GetComponent<MonsterScript>().monsterFightStarted){
				if (eyesClosedDuration < eyesClosedDurationNeeded){
					Debug.Log("closed: "+eyesClosedDuration + " needed: "+eyesClosedDurationNeeded);
					monster.GetComponent<MonsterScript>().PlayerDies();
				} else {
					Debug.Log("closed: "+eyesClosedDuration + " needed: "+eyesClosedDurationNeeded);
					monster.GetComponent<MonsterScript>().MonsterDefeated();
				}
			}

			eyesClosedDuration = 0;
		}

		//Zeit mit geschlossenen Augen
		if (getEyesClosed()) {
			eyesClosedDuration = Time.time - eyesClosedTimepoint;
		}

	}

	// return eyesClosed
	public bool getEyesClosed ()
	{
		return eyesAnimator.GetBool("EyesClosed");
	}
}