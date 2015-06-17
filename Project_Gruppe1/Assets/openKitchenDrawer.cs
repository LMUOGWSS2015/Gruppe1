

using UnityEngine;
using System.Collections;

public class openKitchenDrawer : MonoBehaviour {
	
	private AudioSource doorSound;
	
	
	public AudioClip openDrawerSound;
	public AudioClip closeDrawerSound;
	
	
	public bool open = false;
	
	public float smooth = 5f;

	private Vector3 openPosition;
	private Vector3 closePosition;
	
	// Use this for initialization
	void Start () {
		doorSound = GetComponent<AudioSource>();

		openPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 0.5f);
		closePosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

		if (open) {
			transform.localPosition = Vector3.Lerp(closePosition, openPosition, Time.deltaTime  * smooth);
		}
	}
	
	public void ChangeDrawerState() {
		open = !open;
		if (open) {
			doorSound.clip = openDrawerSound;
		} else {
			doorSound.clip = closeDrawerSound;
		}
		doorSound.Play ();
	}
	// Update is called once per frame
	void Update () {
		if (open) {
			transform.localPosition = Vector3.Lerp(transform.localPosition, openPosition, Time.deltaTime  * smooth);
		} else {
			transform.localPosition = Vector3.Lerp(transform.localPosition, closePosition, Time.deltaTime  * smooth);
		}
	}
	
}
