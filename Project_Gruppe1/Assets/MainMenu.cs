using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using iView;

public class MainMenu : MonoBehaviour {
	public GUISkin skin;
	
	// Bianka:
	public Texture2D mainMenuBackgroundImage;
	public Texture2D exitMenuBackgroundImage;
	public GameObject mainMenuMusic;
	//public AudioClip menuSound;
	
	// For different resolutions
	private int guiFactor;

	private bool isExitMenu = false;

	private AudioSource mainMenuAudio;

	void Awake() {
		
		Time.timeScale = 1.0f;

		guiFactor = (int) Mathf.Floor (Screen.width/1024);
		guiFactor = (guiFactor == 0) ? 1 : guiFactor;
		
		mainMenuAudio = mainMenuMusic.GetComponent<AudioSource> ();
		LockCursor (false);
	//	mainMenuAudio.Play ();
	}
	
	void Start() {
		
	}
	
	void OnGUI () {
		if (skin != null) {
			GUI.skin = skin;
		}
		GUI.DrawTexture (new Rect(0,0,Screen.width , Screen.height),mainMenuBackgroundImage);
		if (isExitMenu) {
			CreateExitMenu ();
		} else {
			CreateMainMenu ();
		}
	}
	
	void BeginPage(int width, int height) {
		GUILayout.BeginArea( new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
	}
	
	void EndPage() {
		GUILayout.EndArea();
	}
	
	void CreateMainMenu() {
		BeginPage(800*guiFactor,600*guiFactor);
		GUIStyle guiStyle = new GUIStyle (GUI.skin.label);
		guiStyle.fontSize  *= guiFactor;
		
		GUIStyle buttonStyle = new GUIStyle (GUI.skin.button);
		buttonStyle.alignment = TextAnchor.MiddleLeft;
		buttonStyle.fontSize *= guiFactor;
		
		
		GUI.Label (new Rect (0, 0, 800 * guiFactor, 600 * guiFactor), "DON'T LOOK AT ME!", guiStyle);
		
		if (GUI.Button (new Rect(0, 180*guiFactor, 200*guiFactor, 60*guiFactor), "Play", buttonStyle)) { 
			Application.LoadLevel(1);
			LockCursor(true);
		}

		if (GazeInteractions.useEyeTracking) {
			if (GUI.Button (new Rect (0, 280 * guiFactor, 350 * guiFactor, 60 * guiFactor), "Calibration", buttonStyle)) { 
				SMIGazeController.Instance.StartCalibration (5);
			}
		}
		if (GUI.Button (new Rect(0, GazeInteractions.useEyeTracking ? 380*guiFactor : 280*guiFactor, 200*guiFactor, 60*guiFactor), "Exit", buttonStyle)) { 
			isExitMenu = true;
		}

		EndPage();
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
	
	void CreateExitMenu() {
		BeginPage(500*guiFactor,250*guiFactor);
		GUILayout.Box (exitMenuBackgroundImage);
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
			isExitMenu = false;
		}
		EndPage();
	}
}
