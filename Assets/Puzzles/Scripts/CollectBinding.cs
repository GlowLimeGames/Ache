using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBinding : MonoBehaviour {

	public GameObject binding;
	// Use this for initialization

	void OnCollisionEnter2D(Collision2D coll){
        Destroy(binding);
        Inventory.Instance.RemoveItem(0);
        Inventory.Instance.RemoveItem(1);
        Inventory.Instance.RemoveItem(3);

        AudioController.Instance.PlaySFX("cutting sound");
        Inventory.Instance.AddItem(4);
	}
}