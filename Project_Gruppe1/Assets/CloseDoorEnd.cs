using UnityEngine;
using System.Collections;

public class CloseDoorEnd : MonoBehaviour {

	public GameObject monsterprefab;
	public bool trigger = true;
	private bool startKnocking = false;
	private int numberOfKnocks = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (trigger == false && startKnocking == true && GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().open == false
		    && this.GetComponent<AudioSource>().isPlaying == false) {
			GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().hitAgainstDoor ();
			startKnocking = false;
			Invoke("delayKnock", Random.Range(0.6F, 1.3F));
		}
	}

	void OnTriggerEnter(Collider other) {
		if (GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().open == true && trigger == true) {
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
		numberOfKnocks++;
		if (numberOfKnocks < 5) {
			knockDoor ();
		} else {
			GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().kickOpenDoor();
		}
	}

	public void restartEnding() {
		trigger = true;
		startKnocking = true;
		this.GetComponent<StandardSoundEffectScript> ().restart ();
	}
}
