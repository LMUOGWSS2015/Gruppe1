using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu1 : MonoBehaviour {
	public GUISkin skin;

	private float startTime = 0.1f;

	private float savedTimeScale;
	private bool savedFPSControllerEnabled;

	// Bianka:
	public Texture2D exitMenuOverlayImage;
	public Texture2D mainMenuBackgroundImage;
//	public MovieTexture background;
	public Texture hintImage1;
	public Texture hintImage2;
	public Texture hintImage3;
	public Texture hintImage4;
	public Texture defaultHintImage;
	public GameObject pauseMusic;

	// For different resolutions
	private int guiFactor;
	
	private int foundHints = 0;
	private AudioSource pauseAudio;

//	private GameObject backgroundMoviePlane;
	
	
	public enum Page {
		None,Main,Exit
	}
	
	private Page currentPage;

	void Awake() {
		guiFactor = (int) Mathf.Floor (Screen.width/1024);
		guiFactor = (guiFactor == 0) ? 1 : guiFactor;
		
		pauseAudio = pauseMusic.GetComponent<AudioSource> ();
		
		//Time.timeScale = 1;
	}

	void Start() {
		AudioListener.pause = false;
		pauseAudio.ignoreListenerPause = true;
	}

	int getCountHints() {
		InteractionScript iaScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<InteractionScript> ();
		return iaScript.GetFoundHints ();
	}

	void StartMusic() {
		AudioListener.pause = true;
		/*
		AudioSource[] audios = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource aud in audios) {
			aud.Pause(); 
		}
		*/
		pauseAudio.Play (); 
	}

	void StopMusic() {

		AudioListener.pause = false;
		/*
		AudioSource[] audios = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource aud in audios) {
			if (aud.playOnAwake) {
				aud.Play(); 
			}
		}
		*/
		pauseAudio.Stop ();
	}

	
	void LateUpdate () {
		
		if (Input.GetKeyDown("escape")) 
		{
			switch (currentPage) 
			{
			case Page.None: 
				PauseGame(); 
				break;
				
			case Page.Main: 
				UnPauseGame(); 
				break;
				
			default: 
				currentPage = Page.Main;
				break;
			}
		}
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

		if (IsGamePaused ()) {
			GUI.DrawTexture (new Rect(0,0,Screen.width , Screen.height),mainMenuBackgroundImage);

			switch (currentPage) {
			case Page.Main:
				MainPauseMenu ();
				break;
			case Page.Exit:
				ExitMenu ();
				break;
			}
		} 
	}

	void BeginPage(int width, int height) {
		GUILayout.BeginArea( new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
	}
	
	void EndPage() {
		GUILayout.EndArea();
	}

	
	
	void MainPauseMenu() {
		BeginPage(800*guiFactor,600*guiFactor);
		GUIStyle guiStyle = new GUIStyle (GUI.skin.label);
		guiStyle.fontSize  *= guiFactor;
		
		GUIStyle buttonStyle = new GUIStyle (GUI.skin.button);
		buttonStyle.alignment = TextAnchor.MiddleLeft;
		buttonStyle.fontSize *= guiFactor;


		GUI.Label (new Rect (0, 0, 800 * guiFactor, 600 * guiFactor), "MENU", guiStyle);
		
		if (GUI.Button (new Rect(0, 180*guiFactor, 250*guiFactor, 60*guiFactor),"Continue", buttonStyle)) { 
			UnPauseGame();
		}
		if (GUI.Button (new Rect(0, 280*guiFactor, 250*guiFactor, 60*guiFactor), "Restart", buttonStyle)) { 
			//UnPauseGame();
			Application.LoadLevel(0);
		}
		if (GUI.Button (new Rect(0, 380*guiFactor, 250*guiFactor, 60*guiFactor), "Exit", buttonStyle)) { 
			currentPage = Page.Exit;
		}

		GUILayout.BeginArea (new Rect (350*guiFactor, 150*guiFactor, 400*guiFactor, 400*guiFactor));
			GUILayoutOption[] itemImageOptions = new GUILayoutOption[] {GUILayout.Width (200f*guiFactor), GUILayout.Height (200f*guiFactor)};
			GUIStyle itemImageStyle = new GUIStyle (GUI.skin.box);
			itemImageStyle.margin = new RectOffset (0, 0, 0, 0);
			itemImageStyle.padding = new RectOffset (5*guiFactor, 5*guiFactor, 5*guiFactor, 5*guiFactor);
			//itemImageStyle.alignment = TextAnchor.MiddleCenter;

			GUIStyle imageLOStyle = new GUIStyle(itemImageStyle);
			imageLOStyle.alignment = TextAnchor.LowerRight;
			GUIStyle imageLUStyle = new GUIStyle(itemImageStyle);
			imageLUStyle.alignment = TextAnchor.UpperRight;
			GUIStyle imageROStyle = new GUIStyle(itemImageStyle);
			imageROStyle.alignment = TextAnchor.LowerLeft;
			GUIStyle imageRUStyle = new GUIStyle(itemImageStyle);
			imageRUStyle.alignment = TextAnchor.UpperLeft;
			
			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();
			GUILayout.Box ((foundHints >=1 ? hintImage1 : defaultHintImage), imageLOStyle, itemImageOptions);
			GUILayout.Box ((foundHints >=2 ? hintImage2 : defaultHintImage), imageLUStyle, itemImageOptions);
			GUILayout.EndVertical ();
			GUILayout.BeginVertical ();
			GUILayout.Box ((foundHints >=3 ? hintImage3 : defaultHintImage), imageROStyle, itemImageOptions);
			GUILayout.Box ((foundHints >=4 ? hintImage4 : defaultHintImage), imageRUStyle, itemImageOptions);
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal (); 
		GUILayout.EndArea ();
		EndPage();
	}

	void ExitMenu() {
		BeginPage(500*guiFactor,250*guiFactor);
		GUILayout.Box (exitMenuOverlayImage);
		GUIStyle exitLabelStyle = new GUIStyle (GUI.skin.label);
		exitLabelStyle.fontSize = 50*guiFactor;
		exitLabelStyle.alignment = TextAnchor.UpperCenter;

		GUIStyle buttonStyleLeft = new GUIStyle (GUI.skin.button);
		buttonStyleLeft.alignment = TextAnchor.MiddleLeft;
		buttonStyleLeft.fontSize *= guiFactor;

		GUIStyle buttonStyleRight = new GUIStyle (GUI.skin.button);
		buttonStyleRight.alignment = TextAnchor.MiddleRight;
		buttonStyleRight.fontSize *= guiFactor;

		GUI.Label(new Rect(20*guiFactor, 20*guiFactor, 460*guiFactor, 150*guiFactor), "Do you really want to quit?", exitLabelStyle);
		if (GUI.Button (new Rect(20*guiFactor, 170*guiFactor, 150*guiFactor, 80*guiFactor), "yes", buttonStyleLeft)) {
			Application.Quit ();
		}
		if (GUI.Button (new Rect(330*guiFactor, 170*guiFactor, 150*guiFactor, 80*guiFactor), "no", buttonStyleRight)) { 
			currentPage = Page.Main;
		}
		EndPage();
	}

	
	void PauseGame() {
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;  
		savedFPSControllerEnabled = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController> ().enabled;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController> ().enabled = false;
		foundHints = getCountHints ();
		StartMusic ();
		LockCursor (false);
		currentPage = Page.Main;
	}
	
	void UnPauseGame() {
		Time.timeScale = savedTimeScale;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController> ().enabled = savedFPSControllerEnabled;
		LockCursor (true);
		StopMusic ();
		currentPage = Page.None;
	}
	
	bool IsGamePaused() {
		return (Time.timeScale == 0);
	}
}
