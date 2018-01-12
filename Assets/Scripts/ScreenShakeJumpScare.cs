using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeJumpScare : MonoBehaviour {

	//Reference used to create this class:
	//http://newbquest.com/2014/06/the-art-of
	//-screenshake-with-unity-2d-script/

	public Camera mainCamera;
	private Vector3 originalCameraPosition;
	private float shakeAmt = 0;


//	// Use this for initialization
//	void Start () {
//		
//	}
	
//	// Update is called once per frame
//	void Update () {
//		
//	}

	void OnCollisionEnter2D(Collision2D coll) 
	{

		shakeAmt = coll.relativeVelocity.magnitude * .0025f;
		InvokeRepeating("CameraShake", 0, .01f);
		Invoke("StopShaking", 0.3f);

	}

	void CameraShake()
	{
		if(shakeAmt>0) 
		{
			float quakeAmt = Random.value*shakeAmt*2 - shakeAmt;
			Vector3 pp = mainCamera.transform.position;
			pp.x+= quakeAmt;
			pp.y+= quakeAmt; 
			mainCamera.transform.position = pp;
		}
	}

	void StopShaking()
	{
		CancelInvoke("CameraShake");
		mainCamera.transform.position = originalCameraPosition;
	}
}