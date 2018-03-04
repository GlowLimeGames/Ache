using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartScreen : MonoBehaviour {

	public void Start(){

        AudioController.Instance.PlayMusic("theme2");
		Debug.Log("StartGame");
	}

	public void StartGame() {
        AudioController.Instance.StopMusic();
        SceneManager.LoadScene ("Preface");
        AudioController.Instance.PlayMusic("music box");
    }
		
		
	public void ExitGame()
	{
	Debug.Log ("Exit");
	Application.Quit ();

	}

}
