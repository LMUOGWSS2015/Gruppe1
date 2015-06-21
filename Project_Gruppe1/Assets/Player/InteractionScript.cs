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
		
		
		if (Input.GetKeyDown (KeyCode.Mouse0) && showGUIOverlay) {
			showGUIOverlay = false;
		}
		
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		// Player is looking at the doll and activate doll-animations
		var doll = GameObject.FindGameObjectWithTag ("Doll");

		if (Physics.Raycast (ray, out hit, 20f)) {
			
			if (hit.collider.CompareTag("DollBath")) {
				Debug.Log ("Spieler schaut Puppe an");
				doll.GetComponent<Animator>().Play("BathWalk");

			}
			else if (hit.collider.CompareTag ("DollStep")){
				Debug.Log ("Spieler schaut Puppe an der Treppe an");

				doll.GetComponent<Animator>().Play("Step");
			}
			else if (hit.collider.CompareTag ("DollPiano")){
				Debug.Log ("Spieler schaut Puppe im Wohnzimmer an");
				doll.GetComponent<Animator>().Play("Piano");
			}
			else if (hit.collider.CompareTag ("DollWindow")){
				Debug.Log ("Spieler sieht wie Puppe stirbt!");
				doll.GetComponent<Animator>().Play("Window");
			}
			//Debug.Log ("Spieler schaut nicht auf Puppe");

			
			if (hit.collider.CompareTag ("Kakerlaken")) {
				Debug.Log ("Kakerlaken");
				hit.collider.transform.parent.GetComponent<AudioSource> ().Play ();   
				hit.collider.transform.FindChild ("Kakerlake").gameObject.SetActive (true);
			}

			if (hit.collider.CompareTag("Picture")) {
				//Debug.Log("Picture");
				var script = hit.collider.GetComponent<BlendObjectsScript>();
				if (script != null) {
					script.showObject2();
				}
			}

		}
		
		if (Physics.Raycast (ray, out hit, interactDistance)) {
			
			if (hit.collider.transform.parent != null) {
				if (hit.collider.transform.parent.CompareTag ("usable") || hit.collider.CompareTag ("Door")) {
					useIcon.enabled = true;
				} 
			}
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				
				if (hit.collider.CompareTag ("Door")) {
					Debug.Log ("Tür");
					hit.collider.transform.parent.GetComponent<DoorOpenScript> ().ChangeDoorState (gotKey);
				}
				if (hit.collider.CompareTag ("Schublade")) {
					Debug.Log ("Schublade");
					hit.collider.transform.parent.GetComponent<openKitchenDrawer> ().ChangeDrawerState ();
				} else if (hit.collider.CompareTag ("Hint")) {
					Debug.Log ("Hint gefunden");
					foundHints++;
					Debug.Log ("Hinweise nr: " + foundHints);
					if (numberOfHints == foundHints) {
						Debug.Log ("Alle Hinweise da");	
					}	
					showGUIOverlay = true;		
				} else if (hit.collider.CompareTag ("Key")) {
					gotKey = true;
					Debug.Log ("Schlüßel gefunden");
				} else if (hit.collider.CompareTag ("Flashlight")) {
					Debug.Log ("Taschenlampe gefunden");
					Invoke ("turnOnFlashlight", 1.9f);
					
				} else if (hit.collider.CompareTag ("smartphone")) {
					Debug.Log ("Message Smartphone");
				}
				
				if (hit.collider.transform.parent.CompareTag ("usable")) {
					Debug.Log ("usable");
					var script = hit.collider.gameObject.GetComponent<UseObject> ();
					
					if (script != null) {
						script.useObject ();
					}
				}
			}
			
		} else {
		//	Debug.Log("Kakerlaken weg");
			GameObject kakerlaken = GameObject.FindGameObjectWithTag("Kakerlaken");
			kakerlaken.transform.parent.GetComponent<AudioSource>().Stop();   
			kakerlaken.transform.FindChild("Kakerlake").gameObject.SetActive(false);
		}
		
		
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
			GUI.DrawTexture (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400), hint); 
			
			Invoke("hideGUI", 3.0f);
		}
	}
	
	void hideGUI() {
		showGUIOverlay = false;		
	}

	public void PlayerDies(){
		Debug.Log("Dead...");
		Application.LoadLevel(Application.loadedLevel);
	}
	
}
