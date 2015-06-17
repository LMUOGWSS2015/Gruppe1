using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {

	public bool monsterPresent = false, monsterFightStarted = false, eyesClosedForFight = false;
	public float distanceToPlayer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//TODO VERLAGERN
	public void PlayerDies(){
		Debug.Log("Dead...");
	}

	public void MonsterDefeated(){
		Debug.Log("Monster besiegt...");
	}
}
