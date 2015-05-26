using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class ActiveScript : StateMachineBehaviour {
	

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//usercontrols aktivieren
		animator.gameObject.GetComponentInChildren<CharacterController> ().enabled = true;
		animator.gameObject.GetComponentInChildren<FirstPersonController>().enabled = true;

		//kamera resetten, damit sie nach animation an der selben stelle startet
		animator.gameObject.GetComponentInChildren<FirstPersonController> ().m_MouseLook.ResetRotation (
			animator.gameObject.GetComponentInChildren<FirstPersonController>().transform,
			animator.gameObject.GetComponentInChildren<FirstPersonController>().m_Camera.transform
			);

		Debug.Log("enabled");
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//usercontrols deaktivieren
		animator.gameObject.GetComponentInChildren<CharacterController> ().enabled = false;
		animator.gameObject.GetComponentInChildren<FirstPersonController>().enabled = false;

		Debug.Log("disabled");
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
