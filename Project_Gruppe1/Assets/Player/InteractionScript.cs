﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionScript : MonoBehaviour {

	public float interactDistance = 5f;
	public int numberOfHints;
	public Light flashlight;
	public Image useIcon;

	public bool gotKey = false;
	public bool gotFlashlight = false;
	private int foundHints = 0;
	private float strengthOfFlashlight;

	// Use this for initialization
	void Start () {
		strengthOfFlashlight = flashlight.intensity;
		Debug.Log ("Sooooooo stark wird sie! " + strengthOfFlashlight);

		if (gotFlashlight == false) {
			flashlight.intensity = 0f;
		}
	}

	void turnOnFlashlight() {
		gotFlashlight = true;
		flashlight.intensity = strengthOfFlashlight;
	}

	public int GetFoundHints() {
		return foundHints;
	}

	
	// Update is called once per frame
	void Update () {
		useIcon.enabled = false;

		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, interactDistance)) {

			if (hit.collider.transform.parent != null) {
				if (hit.collider.transform.parent.CompareTag("usable") || hit.collider.CompareTag("Door")) {
					useIcon.enabled = true;
				} 
			}

			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				if (hit.collider.CompareTag("Door")){
					Debug.Log("Tür");
					hit.collider.transform.parent.GetComponent<DoorOpenScript> ().ChangeDoorState (gotKey);
				}
				else if (hit.collider.CompareTag("Hint")) {
					Debug.Log("Hint gefunden");
					foundHints++;
					Debug.Log("Hinweise nr: " +  foundHints);
					if (numberOfHints == foundHints) {
						Debug.Log("Alle Hinweise da");
					}			
				}
				else if (hit.collider.CompareTag("Key")) {
					gotKey = true;
					Debug.Log("Schlüßel gefunden");
				}

				else if (hit.collider.CompareTag("Flashlight")) {
					Debug.Log("Taschenlampe gefunden");
					Invoke("turnOnFlashlight",1.9f);

				}

				if (hit.collider.transform.parent.CompareTag("usable")) {
					hit.collider.gameObject.GetComponentInParent<AudioSource>().Play();
					hit.collider.gameObject.SetActive (false);
				}
			}

		}


	}
}