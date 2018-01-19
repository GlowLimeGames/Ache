using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowJumpScareStop : MonoBehaviour {

	//public GameObject player;
	public GameObject shadow;
	private Animator anim;
	//Vector3 playerPos = player.transform.position.x;
	//static Random random = new Random();

	void Start (){
		anim = shadow.GetComponent<Animator> ();

	}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			anim.SetTrigger("ShadowFadeOut");
			StartCoroutine (pause ());
		}
	}

	IEnumerator pause(){
		print(Time.time);
		yield return new WaitForSeconds(2);
		print(Time.time);
		shadow.SetActive (false);
	}
}