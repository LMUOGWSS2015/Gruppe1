using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	// Update is called once per frame
	void Update ()
	{
		if (transform.rotation.eulerAngles.y > 326.0f) 
		{
			transform.Rotate (new Vector3 (0, -10, 0) * Time.deltaTime);
		}
	}
}
