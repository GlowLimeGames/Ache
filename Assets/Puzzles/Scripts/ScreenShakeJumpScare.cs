using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeJumpScare : MonoBehaviour {

	//Reference used to create this class:
	//http://newbquest.com/2014/06/the-art-of
	//-screenshake-with-unity-2d-script/
	public GameObject player;
	public Camera mainCamera;
	private Vector3 originalCameraPosition;
	private float shakeAmt = 0;

	private void Start(){
		originalCameraPosition = mainCamera.transform.position;
	}
	void OnCollisionEnter2D(Collision2D coll) 
	{
		print ("OnCollisionEnter2D called");
		//shakeAmt = coll.relativeVelocity.magnitude * .0025f;
		shakeAmt = 0.5f;
		InvokeRepeating("CameraShake", 0, .01f);
		Invoke("StopShaking", 0.3f);

	}
	void OnTriggerEnter(Collider coll){
		print ("OnTriggerEnter called");
	}
	void CameraShake()
	{
		print ("CameraShake called");
		if(shakeAmt>0) 
		{
			print ("inside if statement");
			float quakeAmt = Random.value*shakeAmt*2 - shakeAmt;
			Vector3 pp = mainCamera.transform.position;
			pp.x+= quakeAmt;
			pp.y+= quakeAmt; 
			mainCamera.transform.position = pp;
		}
	}

	void StopShaking()
	{
		print ("StopShaking called");
		CancelInvoke("CameraShake");
		mainCamera.transform.position = originalCameraPosition;
	}
}