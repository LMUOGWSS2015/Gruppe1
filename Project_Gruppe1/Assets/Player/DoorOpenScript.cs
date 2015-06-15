using UnityEngine;
using System.Collections;

public class DoorOpenScript : MonoBehaviour {

	private AudioSource doorSound;


	public AudioClip openDoorSound;
	public AudioClip closeDoorSound;
	public AudioClip DoorLockedSound;


	public bool open;

	public bool locked;
	public float doorOpenAngle = 90f;
	public float doorCloseAngle = 0f;
	public float smooth = 2f;

	// Use this for initialization
	void Start () {
		doorSound = GetComponent<AudioSource>();

		if (open == true) {
			transform.Rotate(0, doorOpenAngle, 0);
		}
	}

	public void ChangeDoorState(bool gotKey) {
		if (locked == false || gotKey == true) {
			open = !open;
			if (open == true) {
				doorSound.clip = openDoorSound;
			} else {
				doorSound.clip = closeDoorSound;
			}

		} else {
			Debug.Log("Tür verschloßen!");
			doorSound.clip = DoorLockedSound;
		}

		doorSound.Play ();
	}
	
	// Update is called once per frame
	void Update () {
			if (open) {
				Quaternion targetRotation = Quaternion.Euler (0, doorOpenAngle, 0);
				transform.localRotation = Quaternion.Slerp (transform.localRotation, targetRotation, smooth * Time.deltaTime);
			} else {
				Quaternion targetRotation2 = Quaternion.Euler (0, doorCloseAngle, 0);
				transform.localRotation = Quaternion.Slerp (transform.localRotation, targetRotation2, smooth * Time.deltaTime);
			}
	}
}
