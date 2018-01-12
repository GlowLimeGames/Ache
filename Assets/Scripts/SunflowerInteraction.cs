using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerInteraction : MonoBehaviour {

	public GameObject birdSeed;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
			//Drop birdseed so user can click on it to
			//add to inventory
			print ("Bird seed attained from sunflower");
	}
}