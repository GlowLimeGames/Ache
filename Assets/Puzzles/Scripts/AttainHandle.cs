using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttainHandle : MonoBehaviour {

	void OnMouseDown(){
		Inventory.Instance.AddItem (1);
        //Switch to crushed tree sprite and add stick if
        //boulder has collided with tree object
        Destroy(this.gameObject);
	}
}