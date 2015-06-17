using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

/*
 * Script for FPSController
 */
public class Spiderinteraction : MonoBehaviour {

	private EyesScript es;
	private string subtitle;
	public Transform target;
	Quaternion targetRotation; // change it whenever you need to turn
	float turnSpeed = 0f; // internal property
	float turnSpeedChange = 20f; // acceleration of turning
	Transform rotatingTransform; // transform to rotate

	// Use this for initialization
	void Start () 
	{
		es  = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EyesScript>();
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("SpiderCollider")) 
		{
			GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController>().enabled = false;
			GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterController> ().enabled = false;

			GameObject.FindGameObjectWithTag ("Player").transform.LookAt(target);
			//Quaternion rotation = Quaternion.LookRotation (GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>().position);   
			//GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>().rotation = Quaternion.Slerp (GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>().rotation, rotation, Time.deltaTime);

			/*//angle we need to turn
			float angleToTurn = Quaternion.Angle(rotatingTransform.rotation, targetRotation);
			//speed is in degrees/sec = angle, to pass angle in 1 seconds. our speed can be increased only 'turnSpeedChange' degrees/sec^2, but don't increase it if needn't
			turnSpeed = Mathf.Min(angleToTurn, turnSpeed + turnSpeedChange * Time.fixedDeltaTime);
			//rotate
			rotatingTransform.rotation = Quaternion.Lerp(rotatingTransform.rotation, targetRotation, Mathf.Clamp01(angleToTurn > 0 ? turnSpeed * Time.fixedDeltaTime / angleToTurn : 0f));
			*/

			GameObject.FindGameObjectWithTag ("SpiderCollider").SetActive (false);
			GameObject.FindGameObjectWithTag ("Subtitles").GetComponent<Text>().text = "Close your Eyes";
			StartCoroutine(Spiderattack(3));
		}
	}

	IEnumerator Spiderattack(int x) {
		yield return new WaitForSeconds(x);
		if (!es.getEyesClosed ()) {
			GameObject.FindGameObjectWithTag ("Subtitles").GetComponent<Text>().text = "You took too long.";		
			} else {
				GameObject.FindGameObjectWithTag("Spiders").SetActive(false);
			GameObject.FindGameObjectWithTag ("Subtitles").GetComponent<Text>().text = "See? It was only your imagination.";	
			}
		yield return new WaitForSeconds(5);
		GameObject.FindGameObjectWithTag ("Subtitles").GetComponent<Text>().text = "";	
		GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController>().enabled = true;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterController> ().enabled = true;
		}

	void Update() {
		// Rotate the camera every frame so it keeps looking at the target 
		//GameObject.FindGameObjectWithTag ("Player").transform.LookAt(target);
	}
}

