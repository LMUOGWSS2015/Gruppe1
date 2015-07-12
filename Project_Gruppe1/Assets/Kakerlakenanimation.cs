using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Kakerlakenanimation : MonoBehaviour {

	private bool run = false;
	public float smooth = 2f;
	
	private Vector3[] endPositions;
	private bool alreadyRun = false;
	private AudioSource sound;
	private Transform[] kakerlakenTransforms;
	private List<Transform> cockroaches = new List<Transform>();
	private bool soundStarted = false;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource> (); 
		this.myFindChild(this.transform, "Kakerlake"); //find all child objects with tag "Kakerlake"
		kakerlakenTransforms = cockroaches.ToArray();
		endPositions = new Vector3[kakerlakenTransforms.Length];
		for (var i = 0; i < kakerlakenTransforms.Length; i++) {
			kakerlakenTransforms[i].gameObject.SetActive(false);
		}
		endPositions[0] = new Vector3 (kakerlakenTransforms[0].localPosition.x, kakerlakenTransforms[0].localPosition.y - 4.0f, kakerlakenTransforms[0].localPosition.z - 2.0f);
		endPositions[1] = new Vector3 (kakerlakenTransforms[1].localPosition.x, kakerlakenTransforms[1].localPosition.y - 4.0f, kakerlakenTransforms[1].localPosition.z - 0.5f);

		
	}
	
	
	public void AnimateKakerlake() {

		if (!alreadyRun) {
			run = true;

			if(!soundStarted) {
				sound.Play();
				soundStarted = true;
			}
			foreach(Transform kakerlake in kakerlakenTransforms) {
				kakerlake.gameObject.SetActive(true);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (run) {
			for (var i = 0; i < kakerlakenTransforms.Length; i++) {
				kakerlakenTransforms[i].localPosition = Vector3.MoveTowards(kakerlakenTransforms[i].localPosition, endPositions[i], Time.deltaTime  * smooth);
			}
		}
		if (run && kakerlakenTransforms.Length > 0 && kakerlakenTransforms[0].localPosition == (endPositions[0])) {
			run = false;
			alreadyRun = true;
			sound.Stop();
			foreach(Transform kakerlake in kakerlakenTransforms) {
				kakerlake.gameObject.SetActive(false);
			}
		}
	}

	public void myFindChild(Transform tpParent, string tpTag) {
		for(int i = 0; i < tpParent.childCount; i++) { //See all child and see they child
			if (tpParent.GetChild(i).tag == tpTag) { //check tag of child
				cockroaches.Add(tpParent.GetChild(i));
			}
			//See childs of current child
			this.myFindChild(tpParent.GetChild(i), tpTag);
		}
	}


}
