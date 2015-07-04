using UnityEngine;
using System.Collections;

public class waterdropScript : MonoBehaviour {
	private int currentParticleCount;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		if(this.GetComponent<ParticleSystem>().particleCount >= currentParticleCount)
		{
			if (this.GetComponent<AudioSource>().isPlaying == false) {
				this.GetComponent<AudioSource>().Play();
			}
			currentParticleCount = this.GetComponent<ParticleSystem>().particleCount;
		}
	}
}
