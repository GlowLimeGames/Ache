using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtrelleController : MonoBehaviour {

	public Dialogue dialogueManager;
	public GameManager gameManager;

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

		if (!dialogueManager.sceneRunning) {
			// TODO: This assumes you have separate Etrelle objects on Forest/House
			// scenes. Should I just get the scene name instead here?
			string sceneTag = "etrelle_" + gameManager.etrelleForest.ToString ();
			dialogueManager.StartScene (sceneTag);

			if (gameManager.etrelleForest < 6) {
				
				gameManager.etrelleForest++;
			}

			// TODO: etrelleHouse should be modulo however many there are, for looping
		}

		//if (!dialogueManager.sceneRunning) {
		//	if (!gameManager.etrelleTalked) {
		//		dialogueManager.StartScene ("etrelle_first");
		//		gameManager.etrelleTalked = true;
		//	} else {
		//		dialogueManager.StartScene ("etrelle_second");
		//	}
		//}
	}
}
