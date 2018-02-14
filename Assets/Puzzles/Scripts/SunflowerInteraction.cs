using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerInteraction : MonoBehaviour {

	public GameObject birdSeed;

	void Start(){
		birdSeed.SetActive (false);
	}

	void OnMouseDown() {
        //Drop birdseed so user can click on it to
        //add to inventory
        if (birdSeed != null)
        {
            birdSeed.SetActive(true);
        }
		print ("Bird seed attained from sunflower");
	}
}