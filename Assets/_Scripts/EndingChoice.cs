using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingChoice : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void KillEnding()
    {
        SceneManager.LoadScene("TheEnd");
    }

    public void ForgiveEnding()
    {
        SceneManager.LoadScene("ForgivenessEnding");
    }
}
