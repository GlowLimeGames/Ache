using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtrelleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// Do various animations and such
	}

	void OnMouseDown() {
		// Click on the NPC sprite to start dialogue

		// Can use logic here to determine which statement to start with

		if (!Dialogue.Instance.sceneRunning) {
			// TODO: This assumes you have separate Etrelle objects on Forest/House
			// scenes. Should I just get the scene name instead here?
			string sceneTag = "etrelle_" + GameManager.Instance.etrelleForest.ToString ();
			Dialogue.Instance.StartScene (sceneTag);

			if (GameManager.Instance.etrelleForest < 6) {
				
				GameManager.Instance.etrelleForest++;
			}

			// TODO: etrelleHouse should be modulo however many there are, for looping
		}
	}
}
