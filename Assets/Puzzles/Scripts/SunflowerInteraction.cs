using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerInteraction : MonoBehaviour {

	public GameObject birdSeed;
	public GameObject player;
	public Item birdSeedItem;
	public Sprite birdSeedSprite;


	void OnMouseDown() {
		//Drop birdseed so user can click on it to
		//add to inventory
		//Sprite image, string type, int iD, int damage
		birdSeedItem = new Item (birdSeedSprite, "Useable", 0, 0);
		player.GetComponent<Inventory> ().AddItem (birdSeedItem);
		print ("Bird seed attained from sunflower");
	}
}