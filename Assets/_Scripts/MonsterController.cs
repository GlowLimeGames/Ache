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
    private MonsterWeapon weapon;

    public Canvas deathCanvas;

	public enum Action {
		SEEK,
		ATTACK,
		DELAY,
		WOUNDED,
		ROAR
	}

	public Action action;
	public bool rage;
    private bool dying;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		facingLeft = true;
		HP = 6;
		//action = Action.SEEK;
		action = Action.ROAR;
		StartCoroutine ("StartSeek");
        bc = GetComponent<BoxCollider2D>();
        weapon = GetComponentInChildren<MonsterWeapon>();
        weapon.Deactivate();
        deathCanvas.gameObject.SetActive(false);
        dying = false;
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
            weapon.Activate();
			if (rage) {
				anim.Play ("AttackTwice");
			} else {
				anim.Play ("AttackOnce");
			}
            StartCoroutine(DeactivateWeapon());
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

		if ((HP <= 0) && (!dying)) {
            dying = true;
			anim.Play ("Die");
			StartCoroutine (DestroyAfterDeath ());
            AudioController.Instance.PlaySFX("monster death sound");
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

    IEnumerator DeactivateWeapon()
    {
        yield return new WaitForSeconds(0.7f);
        weapon.Deactivate();
    }

	IEnumerator DestroyAfterDeath() {
		yield return new WaitForSeconds (1.0f);
        deathCanvas.gameObject.SetActive(true);
		Destroy (gameObject);
	}
}
