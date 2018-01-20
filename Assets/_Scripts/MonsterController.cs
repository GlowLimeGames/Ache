using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

	public Animator anim;

	public GameObject player;

	public int HP;
	public int pause;

	private float maxSpeed = 3.0f;
	public bool facingLeft;

	private float deltaX;
    private BoxCollider2D bc;

	public enum Action {
		SEEK,
		ATTACK,
		DELAY,
		WOUNDED,
		ROAR
	}

	public Action action;
	public bool rage;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		facingLeft = true;
		HP = 6;
		//action = Action.SEEK;
		action = Action.ROAR;
		StartCoroutine ("StartSeek");
        bc = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		if (action == Action.SEEK) {
            bc.size = new Vector2(0.5f, 0.79f);
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
            bc.size = new Vector2(0.75f, 0.79f);
			if (rage) {
				anim.Play ("AttackTwice");
			} else {
				anim.Play ("AttackOnce");
			}
			action = Action.DELAY;
            //anim.Play("Idle");
			StartCoroutine (StopStun ());
		}
	}

	void LateUpdate() {
		deltaX = (player.transform.position.x - transform.position.x);

		if (Mathf.Abs (deltaX) < 1.2f) {
			if (action != Action.DELAY) {
				action = Action.ATTACK;
			}
		}

		if (HP <= 3) {
			rage = true;
		}

		if (HP <= 0) {
			anim.Play ("Die");
			StartCoroutine (DestroyAfterDeath ());
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
		yield return new WaitForSeconds (1.5f);
        anim.Play("Seek");
		action = Action.SEEK;
	}

	IEnumerator StartSeek() {
		yield return new WaitForSeconds (1.5f);
        anim.Play("Seek");
		action = Action.SEEK;
	}

	IEnumerator DestroyAfterDeath() {
		yield return new WaitForSeconds (1.0f);
		Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//print ("Monster collision happened");
		//print (coll.gameObject);
        if (coll.gameObject.CompareTag("Player"))
        {
            playerMovement player = coll.gameObject.GetComponent<playerMovement>();
            if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                HP -= 1;
                print("Monster HP is " + HP);
                anim.Play("Idle");
                action = Action.DELAY;
                StartCoroutine(StopStun());
            }

        }
	}
}
