using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowJumpScareStart : MonoBehaviour {

	//public GameObject player;
	public GameObject shadow;
	private Animator anim;
	//Vector3 playerPos = player.transform.position.x;
	//static Random random = new Random();

	void Start (){
		anim = shadow.GetComponent<Animator> ();
	}
	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Player") {
			//shadow.GetComponent<Renderer>().enabled = false;
			//			if (shadow.Renderer.isVisible () == false) {
			//				//Have shadow pop up x distance away from player
			//				Vector3 position = new Vector3 (Random.Range (-10.0f, 10.0f), Random.Range (-10.0f, 10.0f), 0);
			//				Instantiate (shadow, position, Quaternion.identity);
			//			} 
			//			else if (shadow.isVisible () == false) {
			//
			//			}
			anim.SetTrigger("ShadowFadeOut");

		}
	}
}


//	public GameObject player;
//	public GameObject shadow;
//	//Vector3 playerPos = player.transform.position.x;
//	//static Random random = new Random();
//
//	void OnCollisionEnter(Collision coll){
//		if (coll.gameObject.tag == "Player") {
//			shadow.GetComponent<Renderer>().enabled = true;
//			print ("Shadow appears");
////			if (shadow.Renderer.isVisible () == false) {
////				//Have shadow pop up x distance away from player
////				Vector3 position = new Vector3 (Random.Range (-10.0f, 10.0f), Random.Range (-10.0f, 10.0f), 0);
////				Instantiate (shadow, position, Quaternion.identity);
////			} 
////			else if (shadow.isVisible () == false) {
////
////			}
//		}
//	}
//
//	void OnTriggerEnter(Collider coll){
////		StartCoroutine (ShadowJumpScare_CR ());
//		if (coll.gameObject.tag == "Player") {
//			shadow.GetComponent<Renderer> ().enabled = true;
//			print ("Shadow appears");
//		}
//
//	}
////
////
////	IEnumerator ShadowJumpScare_CR(){
////		//shadow.SetActive(true);
////
////        yield return null;
////	}
////
////	double GetRandomNumber(double minimum, double maximum)
////	{ 
////		return Random.value * (maximum - minimum) + minimum;
////	}
//}