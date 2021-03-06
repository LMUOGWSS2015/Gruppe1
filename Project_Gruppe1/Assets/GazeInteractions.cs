﻿using UnityEngine;
using System.Collections;
using iView;

public class GazeInteractions : MonoBehaviour {

	// Scripts for eyes open / closed
	private EyesScript es;
	private EyesAnimation eyesAniScript;

	private float eyesClosedTimepoint = 0;
	private float eyesClosedDuration = 0;
	private float eyesClosedDurationNeeded;
	
	//Safe last selection of a gazed object
	private GameObject oldSelection;
	
	public GameObject monster;
	public MonsterScript monsterscript;

	// Turneye tracking on
	static public bool useEyeTracking = false;
	
	// Use this for initialization
	void Start () {
		eyesAniScript = GameObject.FindGameObjectWithTag("EyesOverlay").GetComponent<EyesAnimation>();

	}
	
	// Update is called once per frame
	void Update () {
		
		// Track gaze if eye tracking is on
		if (useEyeTracking) {
			eyeTracking ();
		}
		
	}
	
	/*
	 * Track eyes and start interactions
	 */
	void eyeTracking() {
		
	/*	//get the Sample from the Server
		SampleData sample = SMIGazeController.Instance.GetSample();
		
		//get the averaged GazePosition
		Vector3 averageGazePosition = sample.averagedEye.gazePosInUnityScreenCoords ();
		
		//		Debug.Log ("AveragePos (Unity): " + averageGazePosition.x);
		
		// Check if eyes are closed
		if (averageGazePosition.x == 0) {
			
			// Eyes are closed or out of tracker sight
			eyesAniScript.CloseEyes();
			eyesClosedTimepoint = Time.time;
			
			if (monster && monsterscript.monsterFightStarted){
				float distance = monsterscript.distanceToPlayer;
				float timefactor = 0.6f;
				//berechne, wie lange augen geschlossen bleiben muessen
				eyesClosedDurationNeeded = (20f - distance)*timefactor + 2f;
				Debug.Log("Augen sollten " + eyesClosedDurationNeeded + " secs zu sein.");
				
				//deaktiviere endanimation damit man nicht stirbt, waehrend augen zu sind
				monsterscript.playEndAnimation = false;
			}
		} else if (eyesAniScript.eyesClosed){
			// Debug.Log ("Eyes are open.");
			eyesAniScript.OpenEyes();
		}

 */


		Ray rayGaze = Camera.main.ScreenPointToRay(SMIGazeController.Instance.GetSample().averagedEye.gazePosInUnityScreenCoords());
		RaycastHit hit; 
		
		// Raycast from the Gazeposition on the Screen
		if (Physics.Raycast (rayGaze, out hit)) {
			
			// Object gazed/hit: our current selection
			GazeSelectableItem item = hit.collider.gameObject.GetComponent<GazeSelectableItem>();
			
			// If an object is hit continue
			if(item!= null)
			{
				// Check if there is an old selection saved.
				// Then you have to call OnGazeExit() of the old selection before OnGazeEnter()
				if(oldSelection == null)
				{
					// Save current selection (important for OnGazeExit)
					oldSelection = hit.collider.gameObject;
				}
				
				// There is an old selection, 
				// so if it is not the current selection then you leaved it before and you still have to call OnGazeExit() for it
				// and you overwrite the old selection with the current selection
				else if(hit.collider.gameObject != oldSelection)
				{
					oldSelection.GetComponent<GazeSelectableItem>().OnGazeExit();
					oldSelection = hit.collider.gameObject;
				}
				
				// Now you can call OnGazeEnter() of the current selection (now saved as oldSelection)
				oldSelection.GetComponent<GazeSelectableItem>().OnGazeEnter();
			}
			
			// No object is hit but...
			else
			{
				// ...if there is an old selection saved you still have to call OnGazeExit() and set the saved selection to null
				if (oldSelection != null)
				{
					oldSelection.GetComponent<GazeSelectableItem>().OnGazeExit();
					oldSelection = null;
				}
			}
		}
		
		// no result --> Check if there is an older Selection saved
		else
		{
			if (oldSelection != null)
			{
				oldSelection.GetComponent<GazeSelectableItem>().OnGazeExit();
				oldSelection = null;
			}
			
		}


		
	}
}
