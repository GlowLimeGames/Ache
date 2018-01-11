using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

	//public Animator anim;

	public GameObject player;

	public int HP;
	public int pause;

	private float maxSpeed = 3.0f;
	public bool facingLeft;

	private float deltaX;

	public enum Action {
		SEEK,
		ATTACK,
		DELAY,
		WOUNDED
	}

	public Action action;

	// Use this for initialization
	void Start () {
		facingLeft = true;
		HP = 6;
		action = Action.SEEK;
	}
	
	// Update is called once per frame
	void Update () {

		deltaX = (player.transform.position.x - transform.position.x);

		if (Mathf.Abs (deltaX) < 0.5f) {
			if (action != Action.DELAY) {
				action = Action.ATTACK;
			}
		}

		if (action == Action.SEEK) {
			if (_playerToLeft ()) {
				if (!facingLeft) {
					_Flip ();
					facingLeft = true;
				}
				transform.Translate (Time.deltaTime*(-1.0f)*maxSpeed, 0, 0);
			} else {
				if (facingLeft) {
					_Flip ();
					facingLeft = false;
				}
				transform.Translate (Time.deltaTime * maxSpeed, 0, 0);
			}
		} else if (action == Action.ATTACK) {
			GetComponent<Animator> ().Play ("Attack");
			action = Action.DELAY;
			StartCoroutine (StopStun ());
		}
	}

	private void _Flip() {
		transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
	}

	private bool _playerToLeft() {
		if (player.transform.position.x <= transform.position.x) {
			return true;
		} else {
			return false;
		}
	}

	IEnumerator StopStun() {
		yield return new WaitForSeconds (3.0f);
		action = Action.SEEK;
	}
}
