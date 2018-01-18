using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalShardPuzzle : MonoBehaviour {

	public GameObject crow;
	public GameObject birdSeed;
	public GameObject metalShard;

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
		//Add metal shard to inventory
		print ("metal shard attained");
		//Item(Sprite image, string type, int iD, int damage)
		//Item metalShardItem = new Item (metalShard.GetComponent(SpriteRenderer), "Metal Shard", 1, 0);
			//AddItem(metalShardItem);
	}
}