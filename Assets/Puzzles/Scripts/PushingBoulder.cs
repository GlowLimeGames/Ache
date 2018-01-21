using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingBoulder : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Boulder") {
			Vector3 temp = new Vector3(3.0f,0,0);
			coll.transform.position += temp;
		}
	}
}