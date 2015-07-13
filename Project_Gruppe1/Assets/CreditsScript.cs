using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsScript : MonoBehaviour {
	public GUISkin skin;
	public Texture2D creditsBackgroundImage;
	
	public float fadeSpeed = 0.2f;
	private float alpha = 0f;
	private int fadeDir = 1;
	
	private float startTime = 0.1f;
	
	private float savedTimeScale;

	private bool showCredits = false;

	// For different resolutions
	private int guiFactor;
	
	private int foundHints = 0;

	private bool showGameTitle = true;
	private bool hideGameTitle = false;
	private bool showNames = false;
	private bool hideNames = true;
	private bool showCourseInfo = false;
	private bool hideCourseInfo = true;


	void Start() {
		guiFactor = (int) Mathf.Floor (Screen.width/1024);
		guiFactor = (guiFactor == 0) ? 1 : guiFactor;
	}

	public void startCredits() {
		showCredits = true;
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
		titleStyle.fontSize *= guiFactor;

		GUIStyle textStyleContent = new GUIStyle (GUI.skin.label);
		textStyleContent.alignment = TextAnchor.MiddleCenter;
		textStyleContent.fontSize = Mathf.FloorToInt (Mathf.Floor (textStyleContent.fontSize * guiFactor * 0.5f));
		
		GUIStyle textStyleTitle = new GUIStyle (GUI.skin.label);
		textStyleTitle.alignment = TextAnchor.MiddleCenter;
		textStyleTitle.fontSize = Mathf.FloorToInt (Mathf.Floor (textStyleTitle.fontSize * guiFactor * 0.7f));
		if (!hideGameTitle) {
			alpha += fadeDir * fadeSpeed * Time.deltaTime;	
			alpha = Mathf.Clamp01 (alpha);	
			
			Color thisColor = GUI.color;
			thisColor.a = alpha;
			GUI.color = thisColor;

			GUI.Label (new Rect (0, Screen.height/2 -  100 * guiFactor, Screen.width, 200 * guiFactor), "DON'T LOOK AT ME!", titleStyle);

		}
		if (showGameTitle) {
			Invoke ("hideGUIGameTitle", 3.0f);
		}
		else if (!showGameTitle && !(hideGameTitle)) {
			Invoke ("hiddenGUIGameTitle", 3.0f);
		}
		if (!hideNames) {
			alpha += fadeDir * fadeSpeed * Time.deltaTime;	
			alpha = Mathf.Clamp01 (alpha);	
			
			Color thisColor = GUI.color;
			thisColor.a = alpha;
			GUI.color = thisColor;
			
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
			
			GUI.Label (new Rect (0, 100 * guiFactor, Screen.width, heightTitle), "Group members:", textStyleTitle);
			for (var i=0; i<mitgliederArray.Count; i++) {
				GUI.Label (new Rect (0, 100 * guiFactor + heightTitle + (i * heightContent), Screen.width, heightContent), (string)mitgliederArray [i], textStyleContent);
			}
		}
		if (showNames) {
			Invoke ("hideGUINames", 3.0f);
		}
		else if (!showNames && !(hideNames)) {
			Invoke ("hiddenGUINames", 3.0f);
		}
		if (!hideCourseInfo) {
			alpha += fadeDir * fadeSpeed * Time.deltaTime;	
			alpha = Mathf.Clamp01 (alpha);	
			
			Color thisColor = GUI.color;
			thisColor.a = alpha;
			GUI.color = thisColor;
			
			GUI.Label (new Rect (0, Screen.height/2 -  50 * guiFactor, Screen.width, 50 * guiFactor), "Developed within the framework of the", textStyleContent);
			GUI.Label (new Rect (0, Screen.height/2 , Screen.width, 50 * guiFactor), "Open Games Workshop 2015 - LMU Munich", textStyleContent);
			
		}
		if (showCourseInfo) {
			Invoke ("hideGUICourseInfo", 3.0f);
		}
		else if (!showCourseInfo && !(hideCourseInfo)) {
			Invoke ("hiddenGUICourseInfo", 3.0f);
		}
	}
	
	void showGUIGameTitle() {
		fadeDir = 1;
		hideGameTitle = false;
		showGameTitle = true;		
	}
	void hideGUIGameTitle() {
		fadeDir = -1;
		hideGameTitle = false;
		showGameTitle = false;		
	}
	void hiddenGUIGameTitle() {
		hideGameTitle = true;	
		showGUINames ();
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
		showGUICourseInfo ();
	}
	
	void showGUICourseInfo() {
		fadeDir = 1;
		hideCourseInfo = false;
		showCourseInfo = true;		
	}
	void hideGUICourseInfo() {
		fadeDir = -1;
		hideCourseInfo = false;
		showCourseInfo = false;		
	}
	void hiddenGUICourseInfo() {
		hideCourseInfo = true;	
		Application.LoadLevel (0);
	}
}
