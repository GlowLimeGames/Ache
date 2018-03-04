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
		metalShard.SetActive (false);
		anim = crow.GetComponent<Animator> ();
		crowFull = false;
	}

	void OnTriggerEnter2D(Collider2D coll){
		print ("OnCollisionEnter2D Called");
		if (coll.tag == "Player") {
			//Not sure if the following line is necessary
			birdSeed = coll.gameObject;
            AudioController.Instance.StopAllSFX();
            AudioController.Instance.PlaySFX("crow single");

            Fly();
        }
		else if(coll.tag == "Crow"){
			anim.SetTrigger ("StartEating");
			DropShard ();
			crowFull = true;
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
		Inventory.Instance.AddItem(3);
		metalShard.SetActive (true);
		print ("metal shard attained");
	}
}