﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu1 : MonoBehaviour {
	public GUISkin skin;

	private float startTime = 0.1f;

	private float savedTimeScale;

	// Bianka:
	public Texture2D pauseMenuBackgroundImage;
	public Texture2D mainMenuBackgroundImage;
	//public Color buttonColor;
	public Texture hintImage1;
	public Texture hintImage2;
	public Texture hintImage3;
	public Texture hintImage4;
	public Texture defaultHintImage;
	public AudioClip menuSound;

	private int foundHints = 0;
	private AudioSource audio;
	
	
	public enum Page {
		None,Main,Exit
	}
	
	private Page currentPage;

	void Start() {

		audio = gameObject.AddComponent<AudioSource> ();

		Time.timeScale = 1;
		InteractionScript iaScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<InteractionScript> ();
		foundHints = iaScript.GetFoundHints ();
		Debug.Log ("Found Hints: " + foundHints);
		if(IsBeginning())
			PauseGame();
	}

	void StartMusic() {
		audio.volume = .2f;
		audio.clip = menuSound;
		audio.Play ();
		audio.loop = true;
	}

	void StopMusic() {
		audio.Stop ();
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
				if (!IsBeginning()) 
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
			GUIStyle backgroundImageStyle = new GUIStyle (GUI.skin.box);
			GUILayout.Box (IsBeginning () ? mainMenuBackgroundImage : pauseMenuBackgroundImage, backgroundImageStyle);
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
	
	bool IsBeginning() {
		return (Time.time < startTime);
	}
	
	
	void MainPauseMenu() {
		BeginPage(800,600);
		GUILayout.Label (IsBeginning() ? "DON'T LOOK AT ME!" : "MENU");
		if (GUILayout.Button (IsBeginning() ? "Play" : "Continue")) {
			UnPauseGame();
		}
		if (!IsBeginning() && GUILayout.Button ("Restart")) { 
			UnPauseGame();
			Application.LoadLevel(0);
		}
		if (GUILayout.Button ("Exit")) {
			currentPage = Page.Exit;
		}
		if (!IsBeginning ()) {
			GUILayout.BeginArea (new Rect (350, 100, 400, 400));
				GUILayoutOption[] itemImageOptions = new GUILayoutOption[] {GUILayout.Width (200f), GUILayout.Height (200f)};
				GUIStyle itemImageStyle = new GUIStyle (GUI.skin.box);
				itemImageStyle.margin = new RectOffset (0, 0, 0, 0);
				itemImageStyle.padding = new RectOffset (5, 5, 5, 5);
				itemImageStyle.alignment = TextAnchor.UpperRight;
				GUILayout.BeginHorizontal ();
				GUILayout.BeginVertical ();
				GUILayout.Box ((foundHints >=1 ? hintImage1 : defaultHintImage), itemImageStyle, itemImageOptions);
				GUILayout.Box ((foundHints >=2 ? hintImage2 : defaultHintImage), itemImageStyle, itemImageOptions);
				GUILayout.EndVertical ();
				GUILayout.BeginVertical ();
				GUILayout.Box ((foundHints >=3 ? hintImage3 : defaultHintImage), itemImageStyle, itemImageOptions);
				GUILayout.Box ((foundHints >=4 ? hintImage4 : defaultHintImage), itemImageStyle, itemImageOptions);
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal (); 
			GUILayout.EndArea ();
		}
		EndPage();
	}

	void ExitMenu() {
		BeginPage(500,250);
		GUILayout.Box (pauseMenuBackgroundImage);
		GUIStyle exitLabelStyle = new GUIStyle (GUI.skin.label);
		exitLabelStyle.fontSize = 50;
		exitLabelStyle.alignment = TextAnchor.UpperCenter;

		GUIStyle buttonStyleLeft = new GUIStyle (GUI.skin.button);
		buttonStyleLeft.alignment = TextAnchor.MiddleLeft;

		GUIStyle buttonStyleRight = new GUIStyle (GUI.skin.button);
		buttonStyleRight.alignment = TextAnchor.MiddleRight;

		GUI.Label(new Rect(20, 20, 460, 150), "Do you really want to quit?", exitLabelStyle);
		if (GUI.Button (new Rect(20, 170, 150, 80), "yes", buttonStyleLeft)) {
			Application.Quit ();
		}
		if (GUI.Button (new Rect(330, 170, 150, 80), "no", buttonStyleRight)) { 
			currentPage = Page.Main;
		}
		EndPage();
	}

	
	void PauseGame() {
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.pause = true;
		StartMusic ();
		LockCursor (false);
		currentPage = Page.Main;
	}
	
	void UnPauseGame() {
		Time.timeScale = savedTimeScale;
		AudioListener.pause = false;
		StopMusic ();
		LockCursor (true);
		currentPage = Page.None;
	}
	
	bool IsGamePaused() {
		return (Time.timeScale == 0);
	}
	
	void OnApplicationPause(bool pause) {
		if (IsGamePaused()) {
			AudioListener.pause = true;
		}
	}
}