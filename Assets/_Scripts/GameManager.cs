using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Dialogue indices
	public int etrelleForest = 0;
	public int etrelleHouse = 0;
	public bool guardianTalked = false;

	public bool gameplayScene;

	void OnEnable()
	{
		gameplayScene = false;
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	// Use this for initialization
	void Start () {
		// Starts in preload scene, which is not a gameplay scene
		//gameplayScene = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		//// TODO: More than just the preface obviously
		//if (scene.name == "Preface") {
		//	gameplayScene = true;
		//} else {
		//	gameplayScene = false;
		//}

	}
}
