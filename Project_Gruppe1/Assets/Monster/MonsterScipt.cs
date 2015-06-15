using UnityEngine;
using System.Collections;

public class MonsterScipt : MonoBehaviour {

	public float speed = 0;
	float wantedspeed = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (speed < wantedspeed) {
			speed +=0.35f;
		} else if (speed > wantedspeed){
			speed -=0.35f;
		}
	}

	void Slower() {
		wantedspeed = 0.35f;
		speed = wantedspeed;
		//speed = wantedspeed;
	}

	void Faster() {
		wantedspeed = 0.35f * 10f;
		//wantedspeed = speed;
	}

}
