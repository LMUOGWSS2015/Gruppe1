

using UnityEngine;
using System.Collections;

public class openKitchenDrawer : MonoBehaviour {
	
	private AudioSource doorSound;
	
	
	public AudioClip openDrawerSound;
	public AudioClip closeDrawerSound;
	
	
	public bool open;
	
	public float smooth = 2f;
	
	private Vector3 openPosition;
	private Vector3 closePosition;
	
	// Use this for initialization
	void Start () {
		doorSound = GetComponent<AudioSource>();
		
		closePosition = transform.localPosition;
		openPosition = new Vector3 (closePosition.x, closePosition.y, closePosition.z + 0.5f);
		
		closePosition = transform.localPosition;
		openPosition = new Vector3 (closePosition.x, closePosition.y, closePosition.z + 0.5f);
		if (open == true) {
			transform.localPosition = Vector3.Lerp(closePosition, openPosition, Time.deltaTime * smooth);
		}
	}
	
	public void ChangeDrawerState() {
		open = !open;
		if (open == true) {
			doorSound.clip = openDrawerSound;
			transform.localPosition = Vector3.Lerp(closePosition, openPosition, Time.deltaTime * smooth);
		} else {
			doorSound.clip = closeDrawerSound;
			transform.localPosition = Vector3.Lerp(openPosition, closePosition, Time.deltaTime * smooth);
			
		}
		doorSound.Play ();
	}
	
}
