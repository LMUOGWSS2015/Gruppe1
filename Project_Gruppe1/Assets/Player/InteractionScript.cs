using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using iView;


public class InteractionScript : MonoBehaviour {
	
	public float interactDistance = 5f;
	public int numberOfHints;
	public Light flashlight;
	public Image useIcon;
	private Color maxAlpha;
	
	public Texture gray_overlay;
	public float fadeSpeed = 0.2f;
	private float alpha = 0f;
	private int fadeDir = 1;
	public Texture hint1;
	public Texture hint2;
	public Texture hint3;
	public Texture hint4;
	private bool showGUIOverlay = false;
	private bool hideGUIOverlay = true;
	
	public bool gotKey = false;
	public bool gotFlashlight = false;
	private int foundHints = 0;
	private float strengthOfFlashlight;

	private int screenshotCount = 0;
	
	private Ray ray;

	// to reduce flickering of icon
	private bool showIcon = false;
	private Collider lastUsableCollider;

	// position of usable icon
	Vector3 targetPosition; 

	// to smoooth movement of icon
	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;

	private bool firstTimeUsable = true;
	private bool doorKnockEnd = true;
	
	// Use this for initialization
	void Start () {
		maxAlpha = useIcon.color;
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
		// Screenshop by pressing p
		if(Input.GetKeyDown("p")){
			Debug.Log("Screenshot" + screenshotCount + ".png saved");
			Application.CaptureScreenshot("Screenshot" + screenshotCount + ".png", 5);
			screenshotCount++;
		}



		if (!showIcon && useIcon.enabled == true) {
			useIcon.color = Color.Lerp (useIcon.color, Color.clear, 3.5f * Time.deltaTime);

			if (useIcon.color.a <= 0.05) {
				useIcon.enabled = false;
				lastUsableCollider = null;					

				if(firstTimeUsable) {
					GameObject.Find ("Subtitle").GetComponent<Text>().text = "";
				}

			}
		} 

		showIcon = false;

		/*
		if (showIcon) {
			useIcon.enabled = true;
			if (lastUsableCollider != null) {
				GameObject.Find ("Benutzen Icon").GetComponent<RectTransform> ().position = targetPosition;
				//GameObject.Find ("Benutzen Icon").GetComponent<RectTransform> ().position = Camera.main.WorldToScreenPoint (lastUsableCollider.GetComponentInChildren<Renderer> ().bounds.center);
			}
		} */

			

			
		if (Input.GetKeyDown (KeyCode.Mouse0) && showGUIOverlay) {
			showGUIOverlay = false;
		}


		if (GameObject.Find ("EyeTrackingController").GetComponent<GazeInteractions> ().useEyeTracking == true) {
			ray = Camera.main.ScreenPointToRay(SMIGazeController.Instance.GetSample().averagedEye.gazePosInUnityScreenCoords());
		} else {
			ray = new Ray (transform.position, transform.forward);
		}


		RaycastHit hit;

		// Player is looking at the doll and activate doll-animations
		var dollB = GameObject.Find ("DollBath");
		var dollS = GameObject.Find ("DollStep");
		var dollP = GameObject.Find ("DollPiano");
		var dollW = GameObject.Find ("DollRoom");

		if (Physics.Raycast (ray, out hit, 20f)) {

			if (hit.collider.CompareTag("StandardSoundTrigger")) {
				hit.collider.GetComponent<StandardSoundEffectScript>().startSound();
			}


			if (hit.collider.CompareTag("Door") && hit.collider.transform.parent.gameObject.name == "DoorChild" && doorKnockEnd) {
				GameObject.Find ("Trigger End Door").GetComponent<CloseDoorEnd>().knockDoor();
				doorKnockEnd = false;
			}
					
			if (hit.collider.CompareTag("DollBath")) {

				if (GameObject.Find("BathroomSoundeffect").GetComponent<AudioSource>().isPlaying == false) {
					GameObject.Find("BathroomSoundeffect").GetComponent<AudioSource>().Play ();
				}
				//Debug.Log ("Spieler schaut Puppe an");
				dollB.GetComponent<Animator>().Play("BathWalk");


			}
			else if (hit.collider.CompareTag ("DollStep")){
				//Debug.Log ("Spieler schaut Puppe an der Treppe an");

				dollS.GetComponent<Animator>().Play("Step");
			}
			else if (hit.collider.CompareTag ("DollPiano")){
				//Debug.Log ("Spieler schaut Puppe im Wohnzimmer an");
				dollP.GetComponent<Animator>().Play("Piano");
			}
			else if (hit.collider.CompareTag ("DollWindow")){
				//Debug.Log ("Spieler sieht wie Puppe stirbt!");
				GameObject.Find ("schrei puppe").GetComponent<StandardSoundEffectScript>().startSound();
				dollW.GetComponent<Animator>().Play("Window");
			}
			//Debug.Log ("Spieler schaut nicht auf Puppe");

			
			if (hit.collider.CompareTag ("Kakerlaken")) {
				hit.collider.transform.parent.GetComponent<Kakerlakenanimation> ().AnimateKakerlake();
//				hit.collider.transform.parent.GetComponent<AudioSource> ().Play ();   
//				hit.collider.transform.FindChild ("Kakerlake").gameObject.SetActive (true);
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
				if (hit.collider.transform.parent.gameObject.name == "DoorChild" && gotKey == false) {
					Debug.Log ("KinderzimmerTür");
					if (GameObject.Find("Child").GetComponent<AudioSource>().isPlaying == false) {
						GameObject.Find("Child").GetComponent<AudioSource>().Play();
					} 
				}
			
				if ((hit.collider.transform.parent.CompareTag ("usable") || hit.collider.CompareTag ("Door")) 
				    && GameObject.Find ("FPSController").GetComponent<CharacterController> ().enabled == true) {

					showIcon = true;

					
					if (firstTimeUsable) {
						GameObject.Find ("Subtitle").GetComponent<Text>().text = "Press Left Mouse Button to use Objects.";
					}

					if (hit.collider != lastUsableCollider || lastUsableCollider == null) {



						lastUsableCollider = hit.collider;

						if (GameObject.Find ("EyeTrackingController").GetComponent<GazeInteractions> ().useEyeTracking == true) {
							targetPosition = SMIGazeController.Instance.GetSample().averagedEye.gazePosInUnityScreenCoords();
						} else {
							targetPosition = new Vector3 (((Screen.width/2) - 40.0f) , ((Screen.height/2)- 60.0f), 0);
						}
					} 



					/*if (GameObject.Find ("EyeTrackingController").GetComponent<GazeInteractions> ().useEyeTracking == true) {
						Vector3 targetPosition;
						targetPosition = SMIGazeController.Instance.GetSample().averagedEye.gazePosInUnityScreenCoords();
						GameObject.Find ("Benutzen Icon").GetComponent<RectTransform> ().position = Vector3.SmoothDamp(GameObject.Find ("Benutzen Icon").GetComponent<RectTransform> ().position, targetPosition, ref velocity, smoothTime);
					} */

					GameObject.Find ("Benutzen Icon").GetComponent<RectTransform> ().position = targetPosition;

					//Debug.Log("Hier ist icon: " + targetPosition);
					//GameObject.Find ("Benutzen Icon").GetComponent<RectTransform> ().position = Camera.main.WorldToScreenPoint (lastUsableCollider.GetComponentInChildren<Renderer> ().bounds.center);

					useIcon.color = maxAlpha;
					useIcon.enabled = true;

				} 

				}

			}



			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				
				if (hit.collider.CompareTag ("Door")) {
					if(firstTimeUsable) {
						GameObject.Find ("Subtitle").GetComponent<Text>().text = "";
						firstTimeUsable = false;
					}

					Debug.Log ("Tür");
					hit.collider.transform.parent.GetComponent<DoorOpenScript> ().ChangeDoorState (gotKey);
				}
				if (hit.collider.CompareTag ("Schublade")) {
					Debug.Log ("Schublade");
					hit.collider.transform.parent.GetComponent<openKitchenDrawer> ().ChangeDrawerState ();
				} else if (hit.collider.CompareTag ("Hint")) {
					Debug.Log ("Hint gefunden");
					foundHints++;
					Renderer[] renderers = hit.transform.parent.GetComponentsInChildren<Renderer>();
					foreach(Renderer r in renderers) {
						r.enabled = false;
					}
					Debug.Log ("Hinweise nr: " + foundHints);
					if (numberOfHints == foundHints) {
						Debug.Log ("Alle Hinweise da");	
					}	
					showGUI();	
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
					if(firstTimeUsable) {
						GameObject.Find ("Subtitle").GetComponent<Text>().text = "";
						firstTimeUsable = false;
					}

					Debug.Log ("usable");
					var script = hit.collider.gameObject.GetComponent<UseObject> ();
					
					if (script != null) {
						script.useObject ();
					}
				}

			
		} else {
		//	Debug.Log("Kakerlaken weg");
//			GameObject kakerlaken = GameObject.FindGameObjectWithTag("Kakerlaken");
//			kakerlaken.transform.parent.GetComponent<AudioSource>().Stop();   
//			kakerlaken.transform.FindChild("Kakerlake").gameObject.SetActive(false);
		}

		

	}
	
	
	
	void OnGUI() {     
		Texture hint = gray_overlay;
		switch (foundHints) {
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
		if (!hideGUIOverlay) {
			alpha += fadeDir * fadeSpeed * Time.deltaTime;	
			alpha = Mathf.Clamp01 (alpha);	
			
			Color thisColor = GUI.color;
			thisColor.a = alpha;
			GUI.color = thisColor;
			
			GUI.DrawTexture (new Rect (0f, 0f, Screen.width, Screen.height), gray_overlay);
			GUI.DrawTexture (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400), hint); 
			
			if (showGUIOverlay) {
				Invoke ("hideGUI", 3.0f);
			} else if (!showGUIOverlay && !(hideGUIOverlay)) {
				Invoke ("hiddenGUI", 3.0f);
			}
		}
	}

	void showGUI() {
		fadeDir = 1;
		hideGUIOverlay = false;
		showGUIOverlay = true;		
	}
	void hideGUI() {
		fadeDir = -1;
		hideGUIOverlay = false;
		showGUIOverlay = false;		
	}
	void hiddenGUI() {
		hideGUIOverlay = true;		
	}

	public void PlayerDies(){
		Debug.Log("Dead...");
		GameObject.FindGameObjectWithTag ("Monster").
			GetComponent<MonsterScript> ().
				MonsterDefeated ();
		GameObject.Find ("Player").transform.position = GameObject.Find ("PlayerSpawn").transform.position;
		//Application.LoadLevel(Application.loadedLevel);
	}



	
}
