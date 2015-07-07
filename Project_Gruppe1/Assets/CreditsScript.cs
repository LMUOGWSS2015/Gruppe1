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
		
		GUIStyle titleStyle = new GUIStyle (GUI.skin.label);
		titleStyle.alignment = TextAnchor.MiddleCenter;
		titleStyle.fontSize  *= guiFactor;

		GUI.Label (new Rect (0, 0, Screen.width, 200*guiFactor), "DON'T LOOK AT ME!" , titleStyle);

		ArrayList array = new ArrayList ();
		array.Add ("Gruppenmitglieder:");
		array.Add ("Xaver Loeffelholz");
		array.Add ("Jens Fakesch");
		array.Add ("Lüppi Orerreug");
		array.Add ("Benjamin Eder");
		array.Add ("Mai Anh Nguyen");
		array.Add ("Inga Brehm");
		array.Add ("Bianka Roppelt");
		
		
		GUIStyle textStyle = new GUIStyle (GUI.skin.label);
		textStyle.alignment = TextAnchor.MiddleCenter;
		textStyle.fontSize  = Mathf.FloorToInt(Mathf.Floor(textStyle.fontSize * guiFactor * 0.5f));
		
		float height = 70*guiFactor;
		
		for(var i=0;i<array.Count;i++)
		{
			GUI.Label(new Rect(0,200+(i*height),Screen.width,height),(string) array[i], textStyle);
		}
//		GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height * 2));
//
//		GUI.DrawTexture (new Rect(0,0,Screen.width , Screen.height),creditsBackgroundImage);
//
//		GUIStyle titleStyle = new GUIStyle (GUI.skin.label);
//		titleStyle.alignment = TextAnchor.MiddleCenter;
//		titleStyle.fontSize  *= guiFactor;
//		
//		GUIStyle textStyle = new GUIStyle (GUI.skin.label);
//		textStyle.alignment = TextAnchor.MiddleCenter;
//		textStyle.fontSize  = textStyle * guiFactor * 0.5;
//
//		GUI.Label (new Rect (0, 0, Screen.width, 200), "DON'T LOOK AT ME!" , titleStyle);
//		GUI.Label (new Rect (0, 200, Screen.width, 100), "Gruppenmitglieder:" , textStyle);
//		GUI.Label (new Rect (0, 300, Screen.width, 100), "Xaver Loeffelholz" , textStyle);
//		GUI.Label (new Rect (0, 400, Screen.width, 100), "Jens Fakesch" , textStyle);
//		GUI.Label (new Rect (0, 500, Screen.width, 100), "Lüppi Orerreug" , textStyle);
//		GUI.Label (new Rect (0, 600, Screen.width, 100), "Benjamin Eder" , textStyle);
//		GUI.Label (new Rect (0, 700, Screen.width, 100), "Mai Anh Nguyen" , textStyle);
//		GUI.Label (new Rect (0, 800, Screen.width, 100), "Inga Brehm" , textStyle);
//		GUI.Label (new Rect (0, 900, Screen.width, 100), "Bianka Roppelt" , textStyle);
//
//
//		GUILayout.EndArea();
	}
}
