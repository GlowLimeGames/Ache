﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(GoToStart());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator GoToStart()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("StartScreen");
    }
}
