using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEndingDialogue : MonoBehaviour {
    public Dialogue dialogueManager;

	// Use this for initialization
	void Start () {
        //dialogueManager = Object.FindObejctOfType<Dialogue>();
        dialogueManager.StartScene("ending");
	}
	
	// Update is called once per frame
	void Update () {
		if (!dialogueManager.sceneRunning)
        {
            SceneManager.LoadScene("TheEnd");
        }
	}
}
