using UnityEngine;
using System.Collections;

public class CloseDoorEnd : MonoBehaviour {

	public GameObject monsterprefab;
	private bool trigger = true;
	private bool startKnocking = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (trigger == false && startKnocking == true && GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().open == false
		    && this.GetComponent<AudioSource>().isPlaying == false) {
			GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().hitAgainstDoor ();
			startKnocking = false;
			Invoke("delayKnock", 1.5f);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().open == true) {
			GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().ChangeDoorState (true);
		}

		if (trigger == true) {
			GameObject.Find ("FirstPersonCharacter").GetComponent<EyesScript> ().outro = true;
			//spawn monster
			Transform monsterspawn = GameObject.Find ("MonsterSpawnEnde").transform;
			trigger = false;
			GameObject monster = (GameObject)Instantiate (monsterprefab, monsterspawn.position, monsterspawn.rotation);

		}
		//entferne trigger, damit nur einmal ausgeloest
		//Destroy (gameObject);
		//Destroy (this);
	}

	public void knockDoor() {
		startKnocking = true; 
	}

	private void delayKnock() {
		knockDoor ();
	}
}
