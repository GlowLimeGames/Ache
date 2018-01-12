using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowJumpScare : MonoBehaviour {

	public GameObject player;
	public Sprite shadow;
	//static Random random = new Random();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Player") {
			//Have shadow pop up x distance away from player
			//at random time from when trigger has been collided with
			//player.GetComponenet(). 
			//yield return new WaitForSeconds(GetRandomNumber(0.0, 6.0));
			Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
			Instantiate(shadow, position, Quaternion.identity);

		}
	}

	void OnTriggerEnter(Collider coll){
		StartCoroutine (ShadowJumpScare_CR ());
	}


	IEnumerator ShadowJumpScare_CR(){
		//shadow.SetActive(true);

        yield return null;
	}

	double GetRandomNumber(double minimum, double maximum)
	{ 
		return Random.value * (maximum - minimum) + minimum;
	}
}
