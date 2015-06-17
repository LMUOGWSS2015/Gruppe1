using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {

	public bool monsterFightStarted = false, setCloseup = false, playEndAnimation = true;
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
		GameObject.FindGameObjectWithTag ("MonsterAuftritt").GetComponent<MonsterAuftritt> ().ResetFPSController ();
		GetComponentsInChildren<SkinnedMeshRenderer> () [0].enabled = false;
		GetComponentsInChildren<SkinnedMeshRenderer> () [1].enabled = false;
	}
}
