using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePuzzle : MonoBehaviour {

	public GameObject metalShard;
	public GameObject sword;
	public GameObject binding;

	void Start(){
		sword.SetActive (false);
		binding.SetActive (false);

	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Player") {
			print ("rope attained, now they are combined" +
				"to make the sword");
			//Not sure if the following line is necessary
			metalShard = coll.gameObject;
            //remove metal shard from inventory
            binding.SetActive (true);
		}
	}
}