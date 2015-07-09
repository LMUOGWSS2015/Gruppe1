using UnityEngine;
using System.Collections;

public class CloseDoorEnd : MonoBehaviour {

	public GameObject monsterprefab;
	private bool trigger = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().open == true) {
			GameObject.Find ("DoorChild").GetComponent<DoorOpenScript> ().ChangeDoorState (true);
		}

		if (trigger == true) {
			GameObject.Find ("FirstPersonCharacter").GetComponent<EyesScript> ().outro = true;
			//spawn monster
			Transform monsterspawn = GameObject.Find ("MonsterSpawnEnde").transform;
			GameObject monster = (GameObject)Instantiate (monsterprefab, monsterspawn.position, monsterspawn.rotation);
			trigger = false;
		}
		//entferne trigger, damit nur einmal ausgeloest
		//Destroy (gameObject);
		//Destroy (this);
	}
}
