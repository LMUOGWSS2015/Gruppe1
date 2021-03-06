﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroAnimation : MonoBehaviour {

	private float alpha = 0f;
	private float fadeSpeed = 0.4f;
	private bool fade1 = false;
	private bool fade2 = false;
	private bool fadeOut = false;
	public Color text1Color;
	public Color text2Color;
	private float fadeDirection = 1f;
	private bool introFinished = false;

	public bool showIntro;

	private bool playIntroSong = true;


	// Use this for initialization
	void Start () {

		Debug.Log ("Show Intro ist auf" + showIntro);

		GameObject.Find ("TextIntroPart1").GetComponent<Text>().color = new Color (255, 255, 255, alpha);
		GameObject.Find ("TextIntroPart2").GetComponent<Text>().color = new Color (255, 255, 255, alpha);


		if (showIntro == true) {
			if (this.gameObject.GetComponent<AudioSource>().isPlaying == false && playIntroSong) {
				playIntroSong = false;
				this.gameObject.GetComponent<AudioSource>().Play ();
			}
			Invoke ("displayPart1", 2);
			Invoke ("displayPart2", 8);
			Invoke ("fadeOutNow", 10);
		} else {
			afterIntro();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (introFinished == false && showIntro == true) {
			alpha += fadeDirection * fadeSpeed * Time.deltaTime;	
			alpha = Mathf.Clamp (alpha, 0, 255);

			if (fade1 == true) {
				GameObject.Find ("TextIntroPart1").GetComponent<Text> ().color = new Color (255, 255, 255, alpha);
			}

			if (fade2 == true) {
				GameObject.Find ("TextIntroPart2").GetComponent<Text> ().color = new Color (255, 255, 255, alpha);
			}

			if (fadeOut == true) {
				GameObject.Find ("TextIntroPart1").GetComponent<Text> ().color = new Color (255, 255, 255, alpha);
				GameObject.Find ("TextIntroPart2").GetComponent<Text> ().color = new Color (255, 255, 255, alpha);
				if (alpha == 0f) {
					afterIntro();
				}
			}

		}

	}


	void displayPart1() {	
		alpha = 0f;
		fade1 = true;
	}

	void displayPart2() {	
		alpha = 0f;
		fade2 = true;
		fade1 = false;
	}

	void fadeOutNow() {
		fadeDirection = -1f;
		fadeOut = true;

	}

	private void afterIntro() {
		//GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<EyesScript> ().setIntroFinished ();

		//GameObject.FindGameObjectWithTag("EyesOverlay").GetComponent<Animator>().SetBool("EyesClosed", false);
		GameObject.Find ("black").GetComponent<Image>().color = new Color (255, 255, 255, 0);

		introFinished = true;
		GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Animator>().SetTrigger("start");
		GameObject.Find ("Player").GetComponent<AudioSource> ().Play ();
	}

	public bool getIntroFinished() {
		return introFinished;
	}
}
