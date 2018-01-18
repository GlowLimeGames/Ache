using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	static float offsetX = 0.0f;

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Temporary vector
		Vector3 temp = player.transform.position;
		temp.x = temp.x - offsetX;
		temp.y = transform.position.y;  // Keep same y
		temp.z = -10.0f;
		// Assign value to Camera position
		transform.position = temp;
	}
}
