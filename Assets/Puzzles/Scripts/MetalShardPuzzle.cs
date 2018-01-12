using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalShardPuzzle : MonoBehaviour {

	public GameObject crow;
	public GameObject birdSeed;
	public GameObject metalShard;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "BirdSeed") {
			//Not sure if the following line is necessary
			birdSeed = coll.gameObject;
			Fly ();
		}
		else if(coll.gameObject.tag == "Crow"){
			DropShard ();
		}
	}

	//Crow flies towards where on the ground the player placed the birdseed
	private void Fly (){
		crow.transform.position = Vector2.MoveTowards 
			(crow.transform.position, birdSeed.transform.position, 0.5f);
	}

	//Crow drops the shard to eat the birdseed (animation called?) 
	private void DropShard(){
		print ("metal shard attained");
	}
}