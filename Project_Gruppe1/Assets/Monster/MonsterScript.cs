using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class MonsterScript : MonoBehaviour {

	public bool monsterFightStarted = false, setCloseup = false, playEndAnimation = true, walkingStarted = false;
	public float distanceToPlayer = 0;

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<EyesScript> ().monsterscript = this;
		GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<EyesScript> ().monster = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MonsterDefeated(){
		Debug.Log("Monster besiegt...");
		GameObject.Find ("MonsterFeetSound").GetComponent<AudioSource> ().loop = false;
		this.gameObject.GetComponent<MonsterAuftritt> ().ResetFPSController ();
		GetComponentsInChildren<SkinnedMeshRenderer> () [0].enabled = false;
		GetComponentsInChildren<SkinnedMeshRenderer> () [1].enabled = false;
		Destroy (GameObject.Find ("NavDummy"));

		NoiseAndScratches noiseScript = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<NoiseAndScratches> ();
		noiseScript.grainIntensityMin = 0;
		noiseScript.grainIntensityMax = 0;
		noiseScript.scratchIntensityMax = 0;
		noiseScript.scratchIntensityMin = 0;

		InteractionScript scr = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<InteractionScript> ();
		GameObject.Find("FlashlightPlayer").transform.localPosition = scr.flashlightstartPosition;
		GameObject.Find("FlashlightPlayer").transform.localRotation = scr.flashlightstartRotation;
		GameObject.Find("FlashlightPlayer").transform.localScale = scr.flashlightStartScale;

		Destroy (this.gameObject);
		Destroy (this);
	}
}
