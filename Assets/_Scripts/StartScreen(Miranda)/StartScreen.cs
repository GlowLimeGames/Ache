using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartScreen : MonoBehaviour {

	public void Start(){
	

	//Which scene starts the game?
		Debug.Log("StartGame");
	}

	public void StartGame() {
		SceneManager.LoadScene ("Preface");
	}
		
		
	public void ExitGame()
	{
	Debug.Log ("Exit");
	Application.Quit ();

	}

}
