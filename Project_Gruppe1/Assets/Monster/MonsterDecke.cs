using UnityEngine;
using System.Collections;

public class MonsterDecke : MonoBehaviour {

	bool seen = false, fade = false;
	float alpha = 1f;
	Color color;

	// Use this for initialization
	void Start () {
		color = GameObject.Find ("cloth").GetComponent<Renderer> ().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit) && !seen) {
			if (hit.collider.CompareTag ("Monster")) {
				seen = true;
				StartCoroutine(Fade());
			}
		}

		if (fade) {
			color = new Color(color.r,color.g,color.b,alpha);
			GameObject.Find ("cloth").GetComponent<Renderer> ().material.color = color;
			GameObject.Find ("monster").GetComponent<Renderer> ().material.color = color;
			if (alpha - 0.2f >0) {
				alpha -= 0.2f;
			} else {
				alpha = 0;
			}
		}
	}

	IEnumerator Fade() {
		Destroy(this.gameObject, 5f);
		yield return new WaitForSeconds(0.25f);
		gameObject.GetComponentInChildren<ParticleSystem> ().Play ();
		yield return new WaitForSeconds(0.25f);
		fade = true;

	}

}
