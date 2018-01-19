﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingBoulder : MonoBehaviour {

	public GameObject decayingTree;
	public GameObject crushedTree1;
	public GameObject crushedTree2;
	public GameObject crushedTree3;
	public GameObject stick;

	void Start(){
		crushedTree1.SetActive (false);
		crushedTree2.SetActive (false);
		crushedTree3.SetActive (false);
		stick.SetActive (false);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Boulder") {
			Vector3 temp = new Vector3(3.0f,0,0);
			coll.transform.position += temp;
			transform.Rotate(Vector3.right * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		decayingTree.SetActive (false);
		crushedTree1.SetActive (true);
		crushedTree2.SetActive (true);
		crushedTree3.SetActive (true);
		stick.SetActive (true);
	}
}