using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePuzzle : MonoBehaviour {

	public GameObject metalShard;
	public GameObject rope;
	public GameObject sword;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "MetalShard") {
			//Not sure if the following line is necessary
			metalShard = coll.gameObject;
			//remove metal shard from inventory
			print ("rope attained, now they are combined" +
				"to make the sword");
			//Automatically combine to make sword
		}
	}
}