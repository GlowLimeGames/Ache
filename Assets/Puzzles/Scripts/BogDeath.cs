using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BogDeath : MonoBehaviour {

	public GameObject emma;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		print ("OnTriggerEnterCalled");
		if (other.tag == "Player"){ 
			print ("Emma Dead, RIP");
			//other.GetComponent<playerMovement> ().BogKill ();
		}
	}

	void BogKill(){
		//Call die animation

		//Restart bog scene
	}
}