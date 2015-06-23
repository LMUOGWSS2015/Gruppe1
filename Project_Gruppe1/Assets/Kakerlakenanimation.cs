using UnityEngine;
using System.Collections;

public class Kakerlakenanimation : MonoBehaviour {

	private bool run = false;
	public float smooth = 2f;
	
	private Vector3[] endPositions;
	private bool alreadyRun = false;
	private AudioSource sound;
	private GameObject[] kakerlakenObjects;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource> (); 
		kakerlakenObjects = GameObject.FindGameObjectsWithTag ("Kakerlake");
		endPositions = new Vector3[kakerlakenObjects.Length];
		for (var i = 0; i < kakerlakenObjects.Length; i++) {
			kakerlakenObjects[i].SetActive(false);
		}
		endPositions[0] = new Vector3 (kakerlakenObjects[0].transform.localPosition.x, kakerlakenObjects[0].transform.localPosition.y - 4.0f, kakerlakenObjects[0].transform.localPosition.z - 2.0f);
		endPositions[1] = new Vector3 (kakerlakenObjects[1].transform.localPosition.x, kakerlakenObjects[1].transform.localPosition.y - 4.0f, kakerlakenObjects[1].transform.localPosition.z - 0.5f);

		
	}
	
	
	public void AnimateKakerlake() {
		if (!alreadyRun) {
			run = true;
			sound.Play();
			foreach(GameObject kakerlake in kakerlakenObjects) {
				kakerlake.SetActive(true);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (run) {
			for (var i = 0; i < kakerlakenObjects.Length; i++) {
				kakerlakenObjects[i].transform.localPosition = Vector3.MoveTowards(kakerlakenObjects[i].transform.localPosition, endPositions[i], Time.deltaTime  * smooth);
			}
		}
		if (run && kakerlakenObjects.Length > 0 && kakerlakenObjects[0].transform.localPosition == (endPositions[0])) {
			run = false;
			alreadyRun = true;
			sound.Stop();
			foreach(GameObject kakerlake in kakerlakenObjects) {
				kakerlake.SetActive(false);
			}
		}
	}
}
