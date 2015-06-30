using UnityEngine;
using System.Collections;

public class BlendObjectsScript : MonoBehaviour {


	public GameObject objectVisible; //object that is visible initially if player is not looking
	public GameObject objectNotVisible; //object that is not visible initially

	private bool object1 = false; // true if object1 visible
	private float timer = 0; // certain time has to pass before object changes
	private float coolDownTimer = 0;
	private float coolDown = 0.3F;
	private float threashold = 0.5F;
	private float lastLook = 0;
	private bool inView = false;


	// Use this for initialization
	void Start () {
		

		
	}
	
	// Update is called once per frame
	void Update () {
		if (inView && Time.time - lastLook > 3) {
			inView = false;
			showObject1();
		}
		

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


		if (object1) {

			//Debug.Log ("show object 1");
			var script1 = objectVisible.GetComponent<FadeObjectInOut>();
			var script2 = objectNotVisible.GetComponent<FadeObjectInOut>();

			if (script1 != null && script2 != null) {
				script1.FadeIn();
				script2.FadeOut();
			}
			object1 = false;
		} 
	}

	//Show Object2 if not already visible
	public void showObject2() {
		//Debug.Log("visible");
		lastLook = Time.time;
		inView = true;

		if (!object1) {

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
				object1 = true;
				timer = 0;
			}
		}

	}


}
