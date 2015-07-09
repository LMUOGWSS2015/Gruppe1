using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsScript : MonoBehaviour {
	public GUISkin skin;
	public Texture2D creditsBackgroundImage;
	public GameObject creditsMusic;
	
	public float fadeSpeed = 0.2f;
	private float alpha = 0f;
	private int fadeDir = 1;
	
	private float startTime = 0.1f;
	
	private float savedTimeScale;

	private bool showCredits = false;

	// For different resolutions
	private int guiFactor;
	
	private int foundHints = 0;
	private AudioSource creditsAudio;
	
	private bool showNames = true;
	private bool hideNames = false;


	void Start() {
		guiFactor = (int) Mathf.Floor (Screen.width/1024);
		guiFactor = (guiFactor == 0) ? 1 : guiFactor;
		
		creditsAudio = creditsMusic.GetComponent<AudioSource> ();

	}

	/*
	void LateUpdate() {
		// TODO: remove; only for testing
		if (getCountHints() == 2) {
			startCredits();
		}
	}
	*/
	
	int getCountHints() {
		InteractionScript iaScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<InteractionScript> ();
		return iaScript.GetFoundHints ();
	}

	
	void StartMusic() {
		AudioSource[] audios = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource aud in audios) {
			aud.Pause(); 
		}
		creditsAudio.Play ();
	}
	
	void StopMusic() {
		AudioSource[] audios = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource aud in audios) {
			aud.Play(); 
		}
		creditsAudio.Stop ();
	}
	
	
	public void startCredits() {
		showCredits = true;
		StartMusic();
	}
	
	void LockCursor(bool locking) {
		if(locking) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
	

	void OnGUI () {
		if (skin != null) {
			GUI.skin = skin;
		}
		if (showCredits) {
			creditsGUI();
		} 
	}
	
	void creditsGUI() {
		GUI.DrawTexture (new Rect(0,0,Screen.width , Screen.height),creditsBackgroundImage);
		
		if (!hideNames) {
			alpha += fadeDir * fadeSpeed * Time.deltaTime;	
			alpha = Mathf.Clamp01 (alpha);	
			
			Color thisColor = GUI.color;
			thisColor.a = alpha;
			GUI.color = thisColor;
			
			
			GUIStyle titleStyle = new GUIStyle (GUI.skin.label);
			titleStyle.alignment = TextAnchor.MiddleCenter;
			titleStyle.fontSize *= guiFactor;
			
			GUI.Label (new Rect (0, 0, Screen.width, 200 * guiFactor), "DON'T LOOK AT ME!", titleStyle);
			
			
			
			GUIStyle textStyleContent = new GUIStyle (GUI.skin.label);
			textStyleContent.alignment = TextAnchor.MiddleCenter;
			textStyleContent.fontSize = Mathf.FloorToInt (Mathf.Floor (textStyleContent.fontSize * guiFactor * 0.5f));
			
			GUIStyle textStyleTitle = new GUIStyle (GUI.skin.label);
			textStyleTitle.alignment = TextAnchor.MiddleCenter;
			textStyleTitle.fontSize = Mathf.FloorToInt (Mathf.Floor (textStyleTitle.fontSize * guiFactor * 0.7f));
			
			float heightContent = 50 * guiFactor;
			float heightTitle = 90 * guiFactor;
			
			
			ArrayList mitgliederArray = new ArrayList ();
			mitgliederArray.Add ("Xaver Loeffelholz");
			mitgliederArray.Add ("Jens Fakesch");
			mitgliederArray.Add ("Christian Guerrero");
			mitgliederArray.Add ("Benjamin Eder");
			mitgliederArray.Add ("Mai-Anh Nguyen");
			mitgliederArray.Add ("Inga Brehm");
			mitgliederArray.Add ("Bianka Roppelt");
			
			GUI.Label (new Rect (0, 200 * guiFactor, Screen.width, heightTitle), "Gruppenmitglieder:", textStyleTitle);
			for (var i=0; i<mitgliederArray.Count; i++) {
				GUI.Label (new Rect (0, 200 * guiFactor + heightTitle + (i * heightContent), Screen.width, heightContent), (string)mitgliederArray [i], textStyleContent);
			}
		}
		if (showNames) {
			Invoke ("hideGUINames", 3.0f);
		}
		else if (!showNames && !(hideNames)) {
			Invoke ("hiddenGUINames", 3.0f);
		}
	}

	void showGUINames() {
		fadeDir = 1;
		hideNames = false;
		showNames = true;		
	}
	void hideGUINames() {
		fadeDir = -1;
		hideNames = false;
		showNames = false;		
	}
	void hiddenGUINames() {
		hideNames = true;	
		Application.LoadLevel (0);
	}
}
