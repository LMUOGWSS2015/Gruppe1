using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionScript : MonoBehaviour {

	public float interactDistance = 5f;
	public int numberOfHints;
	public Light flashlight;
	public Image useIcon;

	public Texture gray_overlay;
	public float fadeSpeed = 0.2f;
	private float alpha = 0f;
	private int fadeDir = 1;
	public Texture hint1;
	public Texture hint2;
	public Texture hint3;
	public Texture hint4;
	private bool showGUIOverlay = false;


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

	
	void OnGUI() {     
		if (showGUIOverlay) {
			Texture hint = gray_overlay;
			switch(foundHints) {
			case 1: 
				hint = hint1;
				break;
			case 2: 
				hint = hint2;
				break;
			case 3: 
				hint = hint3;
				break;
			case 4: 
				hint = hint4;
				break;
			}
			
			alpha += fadeDir * fadeSpeed * Time.deltaTime;	
			alpha = Mathf.Clamp01(alpha);	

			Color thisColor = GUI.color;
			thisColor.a = alpha;
			GUI.color = thisColor;

			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), gray_overlay);
			GUI.DrawTexture (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), hint); 

			Invoke("hideGUI", 3.0f);
		}
	}

	void hideGUI() {
		showGUIOverlay = false;		
	}


	
	// Update is called once per frame
	void Update () {
		useIcon.enabled = false;

		
		if (Input.GetKeyDown (KeyCode.Mouse0) && showGUIOverlay) {
			showGUIOverlay = false;
		}

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
					showGUIOverlay = true;
					//showNewHint();
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
