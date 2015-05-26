using UnityEngine;
using System.Collections;

public class InteractionScript : MonoBehaviour {

	public float interactDistance = 5f;
	public int numberOfHints;
	public bool gotKey = false;
	private int foundHints = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, interactDistance)) {

				if (hit.collider.CompareTag("Door")){
					Debug.Log("Tür");
					hit.collider.transform.parent.GetComponent<DoorOpenScript> ().ChangeDoorState (gotKey);
				}
				else if (hit.collider.CompareTag("Hint")) {
					Debug.Log("Hint gefunden");
					hit.collider.gameObject.SetActive (false);
					foundHints++;
					Debug.Log("Hinweise nr: " +  foundHints);
					if (numberOfHints == foundHints) {
						Debug.Log("Alle Hinweise da");
					}			
				}
				else if (hit.collider.CompareTag("Key")) {
					gotKey = true;
					Debug.Log("Schlüßel gefunden");
					hit.collider.gameObject.SetActive (false);
				}
			}

		}
	}
}
