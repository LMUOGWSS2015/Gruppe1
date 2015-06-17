using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {

	public bool monsterFightStarted = false, setCloseup = false, playEndAnimation = true, walkingStarted = false;
	public float distanceToPlayer = 0;

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<EyesScript> ().monsterscript = this;
		GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<EyesScript> ().monster = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MonsterDefeated(){
		Debug.Log("Monster besiegt...");
		this.gameObject.GetComponent<MonsterAuftritt> ().ResetFPSController ();
		GetComponentsInChildren<SkinnedMeshRenderer> () [0].enabled = false;
		GetComponentsInChildren<SkinnedMeshRenderer> () [1].enabled = false;
		Destroy (this.gameObject);
	}
}
