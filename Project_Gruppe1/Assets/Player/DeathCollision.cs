using UnityEngine;
using System.Collections;

public class DeathCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Animator>().SetTrigger("die");
		Debug.Log("Dead.");
	}
}
