using UnityEngine;
using System.Collections;

public class BlendObjectsScript : MonoBehaviour {


	public GameObject objectVisible; //object that is visible initially if player is not looking
	public GameObject objectNotVisible; //object that is not visible initially

	private bool inView = false; // true if player is looking on object
	private float timer = 0; // certain time has to pass before object changes
	private float coolDownTimer = 0;
	private float coolDown = 0.3F;
	private float threashold = 1.0F;


	// Use this for initialization
	void Start () {
		

		
	}
	
	// Update is called once per frame
	void Update () {
		

	}

	
	/*public void changeObj() {
		
		
		Debug.Log ("change object: " + inView);

		var script1 = objectVisible.GetComponent<FadeObjectInOut>();
		var script2 = objectNotVisible.GetComponent<FadeObjectInOut>();

		if (inView) {
			if (script1 != null && script2 != null) {
				script1.FadeIn();
				script2.FadeOut();
			}
			inView = false;
		} else {
			if (script1 != null && script2 != null) {
				script1.FadeOut();
				script2.FadeIn();
			}
			inView = true;
		}
	}*/


	//Show Object1 if not already visible
	public void showObject1() {

		if (inView) {

			//Debug.Log ("show object 1");
			var script1 = objectVisible.GetComponent<FadeObjectInOut>();
			var script2 = objectNotVisible.GetComponent<FadeObjectInOut>();

			if (script1 != null && script2 != null) {
				script1.FadeIn();
				script2.FadeOut();
			}
			inView = false;
		} 
	}

	//Show Object2 if not already visible
	public void showObject2() {
		//Debug.Log("visible");

		if (!inView) {

			if(timer == 0){
				timer = Time.time;
			}
			if (Time.time - coolDownTimer > coolDown) {
				timer = 0;
			}
			coolDownTimer = Time.time;
			/*Debug.Log ("CoolDownTimer: " + coolDownTimer);
			Debug.Log ("Timer: " + timer);
			Debug.Log ("Time: " + Time.time);*/
			if (Time.time - timer > threashold && timer != 0) {
				//Debug.Log ("show object 2");

				var script1 = objectVisible.GetComponent<FadeObjectInOut>();
				var script2 = objectNotVisible.GetComponent<FadeObjectInOut>();
			
				if (script1 != null && script2 != null) {
					script1.FadeOut();
					script2.FadeIn();
				}
				inView = true;
				timer = 0;
			}
		}

	}


}
