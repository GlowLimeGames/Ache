using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehavior : MonoBehaviour {

	public Sprite decayingTree;
	public Sprite crushedTree;
	public Sprite stick;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Tree") {
			//Switch to crushed tree sprite and add stick if
			//boulder has collided with tree object
			print("handle attained");
		}
	}
}