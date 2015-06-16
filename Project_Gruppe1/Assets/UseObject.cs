

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
		if(transform.parent.GetComponent<AudioSource>()) {
			transform.parent.GetComponent<AudioSource>().Play();
		}
		if (pickUpObject == true) {
			gameObject.SetActive (false);
		}
	}
}