using UnityEngine;
using System.Collections;

public class openKitchenDrawer : MonoBehaviour {
	
	private AudioSource doorSound;
	
	
	public AudioClip openDrawerSound;
	public AudioClip closeDrawerSound;
	
	
	public bool open;

	public float smooth = 2f;
	
	// Use this for initialization
	void Start () {
		doorSound = GetComponent<AudioSource>();
		
		if (open == true) {
			transform.Translate(0,0,1);
		}
	}
	
	public void ChangeDoorState() {
		open = !open;
		if (open == true) {
			doorSound.clip = openDrawerSound;
		} else {
			doorSound.clip = closeDrawerSound;
		}
		doorSound.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (open) {
			//Quaternion targetPosition = Quaternion.Euler (0, 0, 1f);  
			Vector3 targetPosition = new Vector3(0,0,1f);
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
		} else {
			//Quaternion targetPosition2 = Quaternion.Euler (0, 0, -1f);
			Vector3 targetPosition2 = new Vector3(0,0,-1f);
			transform.position = Vector3.Lerp(transform.position, targetPosition2, Time.deltaTime * smooth);
		}
	}
}
