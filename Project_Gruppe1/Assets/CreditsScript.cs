using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsScript : MonoBehaviour {
	public GUISkin skin;
	public Texture2D creditsBackgroundImage;
	public GameObject creditsMusic;
	
	private float startTime = 0.1f;
	
	private float savedTimeScale;

	private bool showCredits = false;

	// For different resolutions
	private int guiFactor;
	
	private int foundHints = 0;
	private AudioSource creditsAudio;
	


	
	void Start() {
		guiFactor = (int) Mathf.Floor (Screen.width/1024);
		guiFactor = (guiFactor == 0) ? 1 : guiFactor;
		
		creditsAudio = creditsMusic.GetComponent<AudioSource> ();
	}
	
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
	
	
	void LateUpdate () {
		if (getCountHints () == 4) {
			showCredits = true;
			StartMusic();
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
		if (showCredits) {
			creditsGUI();
		} 
	}

	void creditsGUI() {
		GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height * 2));

		GUI.DrawTexture (new Rect(0,0,Screen.width , Screen.height),creditsBackgroundImage);

		GUIStyle guiStyle = new GUIStyle (GUI.skin.label);
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize  *= guiFactor;
		
		GUI.Label (new Rect (0, 0, Screen.width, Screen.height), "DON'T LOOK AT ME!" , guiStyle);


		GUILayout.EndArea();
	}
}
