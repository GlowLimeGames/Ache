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

		if (!dialogueManager.sceneRunning) {
			dialogueManager.StartScene ();
		}

		// Really should store Dialogue.cs in a DialogueController or something
	}
}
