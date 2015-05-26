using UnityEngine;
using System.Collections;

public class DoorOpenScript : MonoBehaviour {

	private bool open = false;
	public bool locked;
	public float doorOpenAngle = 90f;
	public float doorCloseAngle = 0f;
	public float smooth = 2f;

	// Use this for initialization
	void Start () {
	
	}

	public void ChangeDoorState(bool gotKey) {
		if (locked == false || gotKey == true) {
			open = !open;
		} else {
			Debug.Log("Tür verschloßen!");
		}
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
