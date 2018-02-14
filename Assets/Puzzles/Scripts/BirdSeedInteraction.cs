using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSeedInteraction : MonoBehaviour {

	void OnMouseDown() {
		print ("Bird seed added to inventory");
		//Adds birdseed to inventory
		Inventory.Instance.AddItem(2);
        Destroy(this.gameObject);
	}
}
