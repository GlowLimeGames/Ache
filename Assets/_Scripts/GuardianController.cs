using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianController : MonoBehaviour {

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

		print ("It was clicked");

		if (!Dialogue.Instance.sceneRunning) {
			
			if (GameManager.Instance.guardianTalked) {
				sceneTag = "prefaceEnd";
			} else {
				sceneTag = "preface";
			}

			Dialogue.Instance.StartScene (sceneTag);
			GameManager.Instance.guardianTalked = true;
		}
	}
}
