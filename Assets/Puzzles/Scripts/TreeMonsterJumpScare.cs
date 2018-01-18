using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMonsterJumpScare : MonoBehaviour {

	public GameObject treeMonster;
	private Animator anim;

	void Start (){
		anim = treeMonster.GetComponent<Animator> ();
	}

//	void OnCollisionEnter(Collision coll){
//		print ("Trigger Set Off");
//		//if (coll.gameObject.tag == "Player") {
//			anim.SetTrigger("TreeMonsterJumpScare");
//		//}
//	}

	void OnTriggerEnter(Collider coll){
		Debug.Log ("Trigger Set Off");
		//if (coll.gameObject.tag == "Player") {
			anim.SetTrigger ("TreeMonsterJumpScare");
		//}
	}
}