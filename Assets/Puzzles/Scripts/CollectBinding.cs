using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBinding : MonoBehaviour {

	public GameObject binding;
	// Use this for initialization
	void Start () {
		Inventory.Instance.AddItem (2);
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "MetalShard") {
			Destroy (binding);
			Inventory.Instance.AddItem (4);
		}
	}
}