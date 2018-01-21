using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentHouseInteraction : MonoBehaviour {

	public GameObject shelter;

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			print ("walked inside shelter");
			//Make shelter transparent
			Color tmp = shelter.GetComponent<SpriteRenderer>().color;
			tmp.a = 0f;
			shelter.GetComponent<SpriteRenderer>().color = tmp;
		}
	}
}