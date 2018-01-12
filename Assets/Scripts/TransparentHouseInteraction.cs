using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentHouseInteraction : MonoBehaviour {

	public GameObject shelter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Player") {
			//Make shelter transparent
			Color tmp = shelter.GetComponent<SpriteRenderer>().color;
			tmp.a = 0f;
			shelter.GetComponent<SpriteRenderer>().color = tmp;
		}
	}
}