using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MonsterAuftritt : MonoBehaviour {

	Vector3 playerpos;
	GameObject monster, player;
	Animator animator;
	float startY;
	bool stehen = false;

	//fpscontroller werte zum zuruecksetzen
	float m_WalkSpeed;
	float m_RunSpeed;
	float XSensitivity;
	float YSensitivity;

	// Use this for initialization
	void Start () {
		monster = GameObject.FindGameObjectWithTag ("Monster");
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = monster.GetComponent<Animator> ();
		animator.applyRootMotion = true;
		startY = monster.transform.position.y;

		//speicher fps startwerte
		FirstPersonController fpsc = player.GetComponent<FirstPersonController> ();
		m_WalkSpeed = fpsc.m_WalkSpeed;
		m_RunSpeed = fpsc.m_RunSpeed;
		XSensitivity = fpsc.m_MouseLook.XSensitivity;
		YSensitivity = fpsc.m_MouseLook.YSensitivity;
	}
	
	// Update is called once per frame
	void Update () {
		playerpos = player.transform.position;

		//wenn monster sich noch bewegt
		if (!stehen) {
			//auf Spieler ausrichten
			monster.transform.LookAt(playerpos + new Vector3 (0, -2.1f, 0));
			//auf Boden setzen
			monster.transform.position = new Vector3 (monster.transform.position.x, startY, monster.transform.position.z);

			float distance = Vector3.Distance(monster.transform.position,playerpos);
			monster.GetComponent<MonsterScript>().distanceToPlayer = distance;
			//damit ende kurz vor dem player kommt
			distance -= 5f;

			//arme heben
			if (distance < 8 && distance >= 0) {
				animator.SetLayerWeight(1, 1- distance/8);
			}

			//ende ausloesen
			if (distance < - 1.3) {
				if (monster.GetComponent<MonsterScript>().playEndAnimation) {
					//endanimation auslösen
					StartEndAnimation();
				}
				else {
					//monster anhalten
					animator.applyRootMotion = false;
				}
			}

			//monster ranteleportieren, wenn augen zu frueh auf
			if (monster.GetComponent<MonsterScript> ().setCloseup) {
				Vector3 vec = monster.transform.position - playerpos;
				Vector3 pointbetween = playerpos + (vec.normalized * 4.3f);
				monster.transform.position =  new Vector3(pointbetween.x, startY, pointbetween.z);
				StartEndAnimation();
			}

		} else {
			//endanimation
			player.GetComponent<CharacterController> ().enabled = false;
			player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
			
			player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation((monster.transform.position + new Vector3(0,2.6f,0)) - player.transform.position), 5f*Time.deltaTime);
			//if (animator.GetLayerWeight(1) > 0){animator.SetLayerWeight(1, animator.GetLayerWeight(1)-0.2f);}
			player.transform.position = Vector3.Lerp(player.transform.position,monster.transform.position + monster.transform.forward*2.2f + monster.transform.up*2.5f, 1.5f*Time.deltaTime);
		}

		//falls fight noch nicht laueft, starte ihn wenn monster nah genug oder gesehen
		if (!monster.GetComponent<MonsterScript> ().monsterFightStarted){

			Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
			RaycastHit hit;
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

	//startet die endanimation
	public void StartEndAnimation(){
		Debug.Log("Endanimation started");
		animator.SetBool("stehen", true);
		animator.applyRootMotion = false;
		stehen = true;
	}

	//startet den fight
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

	//setzt fps controller auf startwerte zurueck
	public void ResetFPSController(){
		FirstPersonController fpsc = player.GetComponent<FirstPersonController> ();
		fpsc.m_WalkSpeed = m_WalkSpeed;
		fpsc.m_RunSpeed = m_RunSpeed;
		fpsc.m_MouseLook.XSensitivity = XSensitivity;
		fpsc.m_MouseLook.YSensitivity = YSensitivity;
	}

}
