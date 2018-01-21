using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSeedInteraction : MonoBehaviour {

	public GameObject birdSeed;


	void OnMouseDown() {
		print ("Bird seed added to inventory");
		//Adds birdseed to inventory
		Inventory.Instance.AddItem(2);
	}
}
