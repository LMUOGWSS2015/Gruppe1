using UnityEngine;
using System.Collections;

public class HideSpot : MonoBehaviour {

	private bool openHideSpot;
	private GameObject go;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		openHideSpot = false;
		go = GameObject.Find("/Grundriss/Erdgeschoss/Versteck/hide_spot_2/door");
		rb = go.GetComponent<Rigidbody>();
		rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Open hide spot
		if (openHideSpot && go.transform.rotation.eulerAngles.y > -1.1f) {
			//go.transform.Rotate (new Vector3 (0, 0, -10) * Time.deltaTime);
		}
	}

	
	void OnTriggerEnter(Collider other) 
	{
		// Game object need tag "Monster"
		if (other.gameObject.CompareTag ("HideSpot")) 
		{
			// Open hide spot
			openHideSpot = true;
			go.transform.rotation = Quaternion.Euler(0, 0, -0.5f);
			rb.isKinematic = false;
			rb.useGravity = true;
			//go.transform.Rotate (new Vector3 (0, -10, 0) * Time.deltaTime);
		}
	}
}
