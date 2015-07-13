using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class subtitlesScript : MonoBehaviour {

		private string lastSubtitle = ""; 
		public bool fadeOut = false;
		public bool fadeIn = false;
		private float depth = 99;

		private float timeFromAnimationstart = 0.0f;

		private Text subtitle;
		
		// Use this for initialization
		void Start () {
			subtitle = this.GetComponent<Text> ();
			//subtitle.depth = depth;
		}
		
		// Update is called once per frame
		void Update () {
		//Debug.Log("fade in animation with text: " + subtitle.text + ", color value = " + subtitle.color);

			if (lastSubtitle != subtitle.text) {
				lastSubtitle = subtitle.text;
				subtitle.color = Color.clear;
				if (subtitle.text != "") {
					fadeInText();
				}
			}
			
			if (fadeIn == true) {
				timeFromAnimationstart += 2.2f * Time.deltaTime;
				subtitle.color = Color.Lerp (Color.clear, Color.white, timeFromAnimationstart);

				if (subtitle.color.a >= 0.95f) {
					timeFromAnimationstart = 0.0f;
					subtitle.color = Color.white;
					fadeIn = false;
				}
			} 

			
			if (fadeOut == true) {
			timeFromAnimationstart += 1.2f * Time.deltaTime;
				subtitle.color = Color.Lerp (Color.white, Color.clear, timeFromAnimationstart);
				if (subtitle.color.a <= 0.05f) {
					timeFromAnimationstart = 0.0f;
					subtitle.color = Color.clear;
					this.GetComponent<Text> ().text = "";
					fadeOut = false;
				}
			}

		}
		
		public void fadeOutText() {
			fadeOut = true;
		}
		
		public void fadeInText() {
			fadeIn = true;
		}
	}
