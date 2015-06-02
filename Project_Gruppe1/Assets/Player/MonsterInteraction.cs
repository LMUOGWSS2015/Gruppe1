using UnityEngine;
using System.Collections;

/*
 * Script for FPSController
 */
public class MonsterInteraction : MonoBehaviour {

	private EyesScript es;

	// Use this for initialization
	void Start () 
	{
		es  = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EyesScript>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider other) 
	{
		// Game object need tag "Monster"
		if (other.gameObject.CompareTag ("Monster")) 
		{
			Debug.Log ("Ahhhh Monster!");
			StartCoroutine(Die(3));
		}
	}

	IEnumerator Die(int x) {
		Debug.Log ("Oh nein, ich sterbe... In " + x + " Sekunden bin ich tot, wenn ich nicht die Augen schliesse.");
		yield return new WaitForSeconds(x);
		if (!es.getEyesClosed ()) 
		{
			GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Animator>().SetTrigger("die");
		}
		Debug.Log ("============================== Gefahr vorüber! Augen können geöffnet werden.");
	}
}
