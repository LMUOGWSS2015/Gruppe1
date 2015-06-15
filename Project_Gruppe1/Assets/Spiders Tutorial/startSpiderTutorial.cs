using UnityEngine;
using System.Collections;

public class startSpiderTutorial : MonoBehaviour {

	void OnTriggerEnter (Collider player) {
		GameObject.FindGameObjectWithTag("Spiders").GetComponent<Animator>().SetTrigger("TutorialTrigger");

	}

}
