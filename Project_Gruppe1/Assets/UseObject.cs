using UnityEngine;
using System.Collections;

public class UseObject : MonoBehaviour {

	public bool pickUpObject;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void useObject() {
		if (pickUpObject == true) {
			gameObject.SetActive (false);
		}
	}
}
