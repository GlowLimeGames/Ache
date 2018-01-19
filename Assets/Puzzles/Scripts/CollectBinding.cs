using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBinding : MonoBehaviour {

	public GameObject sword;
	// Use this for initialization
	void Start () {
		sword.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDown() {
		//Automatically combine to make sword
		sword.SetActive (true);
	}
}
