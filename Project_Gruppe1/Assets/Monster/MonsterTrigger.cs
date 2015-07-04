using UnityEngine;
using System.Collections;

public class MonsterTrigger : MonoBehaviour {
	
	public GameObject monsterprefab;
	MonsterTriggerParent parentScript;
	Transform monsterspawn;

	// Use this for initialization
	void Start () {
		parentScript = gameObject.transform.parent.gameObject.GetComponent<MonsterTriggerParent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {

		if (other.gameObject.CompareTag ("Player")) {
			if (name.Equals ("TriggerRaum") && !parentScript.wasInRoom && !parentScript.monster) {

				GameObject.Find ("TriggerSound").GetComponent<AudioSource>().Play ();

				parentScript.wasInRoom = true;
				Debug.Log ("was in trigger box");
				monsterspawn = gameObject.transform.GetChild(0);
				parentScript.monster = (GameObject) Instantiate(monsterprefab, monsterspawn.position, monsterspawn.rotation);
				Destroy(gameObject);
				Destroy(this);
			} else if (parentScript.wasInRoom){
				if (parentScript.monster && !parentScript.monster.GetComponent<MonsterScript>().walkingStarted){
					Debug.Log ("start monster by trigger");
					parentScript.monster.GetComponent<MonsterAuftritt>().StartWalking();
					parentScript.wasInRoom = false;
					Destroy(gameObject);
					Destroy(this);
				} else {
					Destroy(gameObject);
					Destroy(this);
				}
			}
		}

	}
}
