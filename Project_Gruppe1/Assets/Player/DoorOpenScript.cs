

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

	private bool hit = false;
	private Vector3 hitPosition;
	private Vector3 standardPosition;

	// Use this for initialization
	void Start () {
		doorSound = GetComponent<AudioSource>();
		
		if (open == true) {
			transform.Rotate(0, doorOpenAngle, 0);
		}

		standardPosition = transform.position; 
		hitPosition = transform.position;
		hitPosition.z -= 1.0f;

	}
	
	public void ChangeDoorState(bool gotKey) {

		if (locked == false || gotKey == true) {
			open = !open;
			if (open == true) {
				doorSound.clip = openDoorSound;
			} else {
				doorSound.clip = closeDoorSound;
			}

			doorSound.Play ();
			
		} else {
			hitAgainstDoor();
			Debug.Log("Tür verschloßen!");

		}
		

	}

	public void hitAgainstDoor() {
		Debug.Log("in door object");
		doorSound.clip = DoorLockedSound;
		doorSound.Play ();
		hit = true;
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

		if (hit) {
			transform.position = Vector3.Lerp (transform.position, hitPosition, 0.9f * Time.deltaTime);

		} else {
			transform.position = Vector3.Lerp (transform.position, standardPosition, 0.9f * Time.deltaTime);

		}

		if (transform.position.z <= -6.5f) {
			hit = false;
		}
	} 
}