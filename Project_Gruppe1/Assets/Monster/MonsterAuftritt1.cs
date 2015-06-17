using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MonsterAuftritt1 : MonoBehaviour {

	Vector3 playerpos;
	GameObject monster, player;
	Animator animator;
	float startY;
	bool stehen = false;
	// Use this for initialization
	void Start () {
		monster = GameObject.FindGameObjectWithTag ("Monster");
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = monster.GetComponent<Animator> ();
		animator.applyRootMotion = true;
		startY = monster.transform.position.y;

		monster.GetComponent<MonsterScript> ().monsterPresent = true;
	}
	
	// Update is called once per frame
	void Update () {
		playerpos = player.transform.position;

		if (!stehen) {
			//auf Spieler ausrichten
			monster.transform.LookAt(playerpos + new Vector3 (0, -2.1f, 0));
			//auf Boden setzen
			monster.transform.position = new Vector3 (monster.transform.position.x, startY, monster.transform.position.z);

			float distance = Vector3.Distance(monster.transform.position,playerpos);
			monster.GetComponent<MonsterScript>().distanceToPlayer = distance;
			//damit ende kurz vor dem player kommt
			distance -= 5f;

			if (distance < 8 && distance >= 0) {
				//arme heben
				animator.SetLayerWeight(1, 1- distance/8);
			}

			if (distance < - 1.3) {
				//endanimation auslösen
				animator.SetBool("stehen", true);
				animator.applyRootMotion = false;
				stehen = true;
			}

		} else {
			//endanimation
			player.GetComponent<CharacterController> ().enabled = false;
			player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
			
			player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation((monster.transform.position + new Vector3(0,2.6f,0)) - player.transform.position), 5f*Time.deltaTime);
			//if (animator.GetLayerWeight(1) > 0){animator.SetLayerWeight(1, animator.GetLayerWeight(1)-0.2f);}
			player.transform.position = Vector3.Lerp(player.transform.position,monster.transform.position + monster.transform.forward*2.2f + monster.transform.up*2.5f, 1.5f*Time.deltaTime);
		}

		//fight starten
		Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		RaycastHit hit;
		
		if (!monster.GetComponent<MonsterScript> ().monsterFightStarted){
			//Start wenn Monster gesehen
			if (Physics.Raycast (ray, out hit) && monster.GetComponent<MonsterScript>().distanceToPlayer < 20f) {
				if (hit.collider.CompareTag ("Monster")) {
					Debug.Log ("Monster gesehen");
					StartFight ();
				}
			}
			//Start wenn Monster nah
			if (monster.GetComponent<MonsterScript>().distanceToPlayer < 7f){
				Debug.Log ("Monster nah");
				StartFight ();
			}
		}
	}

	public void StartFight(){
		monster.GetComponent<MonsterScript>().monsterFightStarted = true;
		Debug.Log("Fight started.");

		//Bewegung einschraenken
		FirstPersonController fpsc = player.GetComponent<FirstPersonController> ();
		fpsc.m_WalkSpeed = 0.3f;
		fpsc.m_RunSpeed = 0.3f;
		fpsc.m_MouseLook.XSensitivity = 0.2f;
		fpsc.m_MouseLook.YSensitivity = 0.2f;

	}

}
