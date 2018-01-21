using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowJumpScareStart : MonoBehaviour {

	//public GameObject player;
	public GameObject shadow;
	private Animator anim;
	//Vector3 playerPos = player.transform.position.x;
	//static Random random = new Random();

	void Start (){
		shadow.SetActive (false);
		anim = shadow.GetComponent<Animator> ();
	}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			shadow.SetActive (true);
			anim.SetTrigger("ShadowPopUp");
		}
	}
}