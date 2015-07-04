using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class MonsterAuftritt : MonoBehaviour {

	Vector3 playerpos;
	GameObject monster, player;
	Animator animator;
	float startY;
	bool closeForAttack = false;

	MonsterScript monsterscript;

	//fpscontroller werte zum zuruecksetzen
	float m_WalkSpeed;
	float m_RunSpeed;
	float XSensitivity;
	float YSensitivity;

	//grainwerte
	float grainmin = 0.05f, grainmax = 0.15f, scratchmin = 0.05f, scratchmax = 0.25f;
	NoiseAndScratches noiseScript;
	bool noiseActive = true;

	// Use this for initialization
	void Start () {
		monster = this.gameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = monster.GetComponent<Animator> ();
		animator.applyRootMotion = false;
		startY = monster.transform.position.y;

		monsterscript = this.gameObject.GetComponent<MonsterScript>();

		noiseScript = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<NoiseAndScratches> ();

		//navdummy aktivieren
		GameObject.Find ("NavDummy").transform.parent = null;


		//speicher fps startwerte
		FirstPersonController fpsc = player.GetComponent<FirstPersonController> ();
		m_WalkSpeed = fpsc.m_WalkSpeed;
		m_RunSpeed = fpsc.m_RunSpeed;
		XSensitivity = fpsc.m_MouseLook.XSensitivity;
		YSensitivity = fpsc.m_MouseLook.YSensitivity;
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance (monster.transform.position, playerpos);
		monster.GetComponent<MonsterScript> ().distanceToPlayer = distance;
		//damit ende kurz vor dem player kommt
		distance -= 5f;

		//arme heben
		if (distance < 8 && distance >= 0) {
			animator.SetLayerWeight (1, 1 - distance / 8);
			if (noiseActive) {

				GameObject.Find ("Rauschen").GetComponent<AudioSource> ().volume = (8.0f-distance)/8.0f;

				noiseScript.grainIntensityMin = Map(grainmin, 0, 0,8f, distance);
				noiseScript.grainIntensityMax = Map(grainmax, 0, 0,8f, distance);

				noiseScript.scratchIntensityMin = Map(scratchmin,0, 0,8f, distance);
				noiseScript.scratchIntensityMax = Map(scratchmax,0, 0,8f, distance);
			}
		}


		Ray ray = new Ray ();
		RaycastHit[] hits = Physics.RaycastAll (Camera.main.transform.position, Camera.main.transform.forward);
		Collider hit = null;
		//trigger herausfiltern und nähstes objekt auswählen
		float smallestDistance = 0;
		for (int i = 0; i < hits.Length; i++) {
			if (!hits[i].collider.isTrigger) {
				if (smallestDistance == 0 || hits[i].distance<smallestDistance){
					smallestDistance = hits[i].distance;
					hit = hits[i].collider;
				}
			}
		}


		if (!monsterscript.walkingStarted) {
			if (hit) {
				Debug.Log(hit.gameObject.name);
				if (hit.CompareTag ("Monster")) {
					GameObject.Find ("MonsterFeetSound").GetComponent<AudioSource> ().loop = true;
					GameObject.Find ("MonsterFeetSound").GetComponent<AudioSource> ().Play ();
					Debug.Log ("Monster seen, start walking");
					StartWalking ();
				}
			}
		} else {
			playerpos = player.transform.position;

			//wenn monster sich noch bewegt
			if (!closeForAttack) {
				//auf Boden setzen
				//monster.transform.position = new Vector3 (monster.transform.position.x, startY, monster.transform.position.z);
				NavMeshAgent navmeshagent = GameObject.Find("NavDummy").GetComponent<NavMeshAgent>();
				navmeshagent.destination = playerpos;

				monster.transform.rotation = GameObject.Find("NavDummy").transform.rotation;
				Vector3 dummypos = new Vector3(monster.transform.position.x, GameObject.Find("NavDummy").transform.position.y,monster.transform.position.z);
				GameObject.Find("NavDummy").transform.position = dummypos;

				//ende ausloesen wenn monster sehr nah
				if (distance < - 1.12) {
					GameObject.Find ("MonsterFeetSound").GetComponent<AudioSource> ().loop = false;

					if (monster.GetComponent<MonsterScript> ().playEndAnimation) {
						//endanimation auslösen
						StartEndAnimation ();

					} else {
						//monster anhalten
						animator.applyRootMotion = false;
					}
				}

				//monster ranteleportieren, wenn augen zu frueh auf
				if (monster.GetComponent<MonsterScript> ().setCloseup) {
					GameObject.Find("SchockSound").GetComponent<AudioSource>().Play();
					Vector3 vec = monster.transform.position - playerpos;
					Vector3 pointbetween = playerpos + (vec.normalized * 4.3f);
					monster.transform.position = new Vector3 (pointbetween.x, startY, pointbetween.z);
					StartEndAnimation ();
				}

			} else { //monster nah genug für endangriff
				//endanimation
				player.GetComponent<CharacterController> ().enabled = false;
				player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
			
				//drehe spielerkamera zu monster
				player.transform.rotation = Quaternion.Slerp (player.transform.rotation, Quaternion.LookRotation ((monster.transform.position + new Vector3 (0, 2.75f, 0)) - player.transform.position), 4.5f * Time.deltaTime);
				//bewege spieler zu monster
				player.transform.position = Vector3.Lerp (player.transform.position, monster.transform.position + monster.transform.forward * 2.6f + monster.transform.up * 2.6f, 1.5f * Time.deltaTime);

				//richte Taschenlampe auf Monster
				Transform flashlightTransform = GameObject.Find("FlashlightPlayer").transform;
				flashlightTransform.rotation = Quaternion.Slerp (flashlightTransform.rotation, Quaternion.LookRotation ((monster.transform.position + new Vector3 (0, 4.5f, 0)) - flashlightTransform.position), 4f * Time.deltaTime);
			}

			//falls fight noch nicht laueft, starte ihn wenn monster nah genug oder gesehen
			if (!monster.GetComponent<MonsterScript> ().monsterFightStarted) {

				//Start wenn Monster gesehen
				if (hit && monster.GetComponent<MonsterScript> ().distanceToPlayer < 20f) {
					if (hit.CompareTag ("Monster")) {
						Debug.Log ("Monster seen");
						StartFight ();
					}
				}
				//Start wenn Monster nah
				if (monster.GetComponent<MonsterScript> ().distanceToPlayer < 5f) {
					Debug.Log ("Monster close");
					StartFight ();
				}
			}
		}
	}

	//startet die endanimation
	public void StartEndAnimation(){
		Debug.Log("Endanimation started");
		animator.SetBool("stehen", true);
		animator.applyRootMotion = false;
		closeForAttack = true;
	}

	//startet den fight
	public void StartFight(){
		monster.GetComponent<MonsterScript>().monsterFightStarted = true;
		Debug.Log("Fight started");

		//Bewegung einschraenken
		FirstPersonController fpsc = player.GetComponent<FirstPersonController> ();
		fpsc.m_WalkSpeed = 0.3f;
		fpsc.m_RunSpeed = 0.3f;
		fpsc.m_MouseLook.XSensitivity = 0.2f;
		fpsc.m_MouseLook.YSensitivity = 0.2f;

		noiseActive = true;
	}

	//setzt fps controller auf startwerte zurueck
	public void ResetFPSController(){
		FirstPersonController fpsc = player.GetComponent<FirstPersonController> ();
		fpsc.m_WalkSpeed = m_WalkSpeed;
		fpsc.m_RunSpeed = m_RunSpeed;
		fpsc.m_MouseLook.XSensitivity = XSensitivity;
		fpsc.m_MouseLook.YSensitivity = YSensitivity;
	}

	public void StartWalking(){
		monsterscript.walkingStarted = true;
		animator.applyRootMotion = true;

	}

	public float Map(float from, float to, float from2, float to2, float value){
		if(value <= from2){
			return from;
		}else if(value >= to2){
			return to;
		}else{
			return (to - from) * ((value - from2) / (to2 - from2)) + from;
		}
	}
}
