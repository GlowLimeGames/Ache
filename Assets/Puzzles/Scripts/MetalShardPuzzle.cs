using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalShardPuzzle : MonoBehaviour {

	public GameObject crow;
	public GameObject birdSeed;
	public GameObject metalShard;
	private Animator anim;
	private bool crowFull;

	void Start (){
		anim = crow.GetComponent<Animator> ();
		crowFull = false;
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "BirdSeed") {
			//Not sure if the following line is necessary
			birdSeed = coll.gameObject;
			Fly ();
		}
		else if(coll.gameObject.tag == "Crow"){
			anim.SetTrigger ("StartEating");
			DropShard ();
			crowFull = true;
			Fly ();
			anim.SetTrigger ("BackInTree");
		}
	}

	//Crow flies towards where on the ground the player placed the birdseed
	private void Fly (){
		if (crowFull == false) {
			anim.SetTrigger ("FlyToBirdSeed");
			crow.transform.position = Vector2.MoveTowards 
			(crow.transform.position, birdSeed.transform.position, 0.5f);
		} 
		else if (crowFull == true) {
			crow.transform.position = Vector2.MoveTowards 
				(birdSeed.transform.position, crow.transform.position, 0.5f);
		}
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