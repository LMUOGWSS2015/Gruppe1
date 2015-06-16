using UnityEngine;
using System.Collections;

public class MonsterAuftritt1 : MonoBehaviour {

	Vector3 playerpos;
	GameObject monster;
	Animator animator;
	float startY;
	// Use this for initialization
	void Start () {
		monster = GameObject.FindGameObjectWithTag ("Monster");
		animator = monster.GetComponent<Animator> ();
		startY = monster.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		playerpos = GameObject.FindGameObjectWithTag ("Player").transform.position;

		monster.transform.LookAt(playerpos + new Vector3(0,-2.1f,0));
		monster.transform.position = new Vector3(monster.transform.position.x, startY, monster.transform.position.z);

		float distance = Vector3.Distance(monster.transform.position,playerpos) - 5f;
		if (distance < 8 && distance > -1) {
			animator.SetLayerWeight(1, 1- distance/8);
		}
	}
}
