using UnityEngine;
using System.Collections;

public class MonsterTrigger : MonoBehaviour {

	static bool wasInRoom = false;
	static GameObject monster;
	public GameObject monsterprefab;
	Transform monsterspawn;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {

		if (other.gameObject.CompareTag ("Player")) {
			if (name.Equals ("TriggerRaum") && !wasInRoom && !monster) {
				wasInRoom = true;
				Debug.Log ("was in trigger box");
				monsterspawn = gameObject.transform.GetChild(0);
				monster = (GameObject) Instantiate(monsterprefab, monsterspawn.position, monsterspawn.rotation);
			} else if (wasInRoom){
				Debug.Log ("start monster by trigger");
				monster.GetComponent<MonsterAuftritt>().StartWalking();
				wasInRoom = false;
			}
		}

	}
}
