using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianController : MonoBehaviour {

	public Dialogue dialogueManager;
	public GameManager gameManager;

	private string sceneTag;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		// Click on the NPC sprite to start dialogue

		// Can use logic here to determine which statement to start with

		if (!dialogueManager.sceneRunning) {
			
			if (gameManager.guardianTalked) {
				sceneTag = "prefaceEnd";
			} else {
				sceneTag = "preface";
			}

			dialogueManager.StartScene (sceneTag);
			gameManager.guardianTalked = true;
		}
	}
}
