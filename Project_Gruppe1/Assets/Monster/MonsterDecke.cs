using UnityEngine;
using System.Collections;

public class MonsterDecke : MonoBehaviour {

	static bool seenfirsttime = false;

	bool seen = false, fade = false;
	float alpha = 1f;
	Color color;

	// Use this for initialization
	void Start () {
		if (seenfirsttime) {
			Destroy (this.gameObject);
			Destroy (this);
		}
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
		gameObject.GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds(0.25f);
		//reposition particles + start
		Vector3 monsterposition = GameObject.Find("Monster an Decke").transform.position + new Vector3(0,4.5f,0);
		Vector3 vec = Camera.main.transform.position - monsterposition;
		Vector3 pointbetween = monsterposition + (vec.normalized * 6f);
		gameObject.GetComponentInChildren<ParticleSystem> ().transform.position = pointbetween;
		gameObject.GetComponentInChildren<ParticleSystem> ().Play ();
		yield return new WaitForSeconds(0.5f);
		fade = true;
		seenfirsttime = true;
	}

}
