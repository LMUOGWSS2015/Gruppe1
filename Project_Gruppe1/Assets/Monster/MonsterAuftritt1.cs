using UnityEngine;
using System.Collections;

public class MonsterAuftritt1 : MonoBehaviour {

	Vector3 goal;
	public float speed;
	GameObject monster;

	// Use this for initialization
	void Start () {
		//goal = GameObject.FindGameObjectWithTag ("Goal").transform.position;
		monster = GameObject.FindGameObjectWithTag ("Monster");
	}
	
	// Update is called once per frame
	void Update () {
		goal = GameObject.FindGameObjectWithTag ("Player").transform.position;
		// The step size is equal to speed times frame time.
		float step = monster.GetComponent<MonsterScipt>().speed * Time.deltaTime;
		monster.transform.position = Vector3.MoveTowards(monster.transform.position, new Vector3(goal.x, monster.transform.position.y, goal.z), step);
		monster.transform.LookAt(goal + new Vector3(0,-2.1f,0));
	}
}
