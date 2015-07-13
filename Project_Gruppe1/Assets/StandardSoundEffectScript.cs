using UnityEngine;
using System.Collections;

public class StandardSoundEffectScript : MonoBehaviour {
		
		public bool playOnlyOnce;
		public float delay;
		private bool playSound = true;
		public bool onGaze = false;
		public bool onEnter = true;
		
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
		void OnTriggerEnter(Collider other) {
			if (onEnter) {
				startSound();
			}
			
		}

	public void startSound() {
		if (this.gameObject.name == "Trigger End Music") {
			if (GameObject.Find ("FirstPersonCharacter").GetComponent<InteractionScript> ().gotKey == false) {
				playSound = false;
			} else {
				playSound = true;
			}
		}

		
		if (playSound == true) {
			if (this.GetComponent<AudioSource> ().isPlaying == false) {
				this.GetComponent<AudioSource> ().PlayDelayed (delay);
			}
		}
		
		if (playOnlyOnce == true) {
			playSound = false;
		}
	}

	public void restart() {
		playSound = true;
	}
		
	}
