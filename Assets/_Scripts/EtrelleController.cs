using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtrelleController : MonoBehaviour {

	public Dialogue dialogueManager;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		// Click on the NPC sprite to start dialogue

		// Can use logic here to determine which statement to start with
		// TODO Use tags and such rather than indices
		if (!dialogueManager.sceneRunning) {
			dialogueManager.StartScene (0);
		}

		// Dialogue json should have properties:
		// speaker
		// line
		// end scene afterwards?
		// set flags?  (quest_given, etc)
		// descriptive tag? (like, "estrelle_first", "estrelle_after_quest", etc)
	}
}
