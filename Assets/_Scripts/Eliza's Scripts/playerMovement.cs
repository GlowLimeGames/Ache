using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour {
	
	float horizontal;
	Rigidbody2D rb; 
	public float playerMovementSpeed; 
	bool facingRight; 
	Vector3 playerScale; 
	public Animator anim; 
	public Transform[] groundPoints; 
	float groundRadius, jumpForce;  
	public LayerMask isGround;
	bool isGrounded, jumped;
    bool crawling = false;
	public int HP;
    bool dying = false;
    BoxCollider2D col2D;
	//public Transform groundCheck; 

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody2D> (); 
		jumpForce = 250;
		groundRadius = .1f;
		facingRight = true;  
		playerMovementSpeed = 3;
        col2D = GetComponent<BoxCollider2D>();
		anim = GetComponent < Animator> (); 

		HP = 3;
	}

	// Update is called once per frame
	void Update () {

		horizontal = Input.GetAxis ("Horizontal"); 
		movePlayer (horizontal); 
		flipPlayer (horizontal);
		isGrounded = IsGrounded ();
        CheckAttack();

		if ((HP <= 0) && (!dying)) {
			anim.SetTrigger ("Die");
            dying = true;
            StartCoroutine(Die());
		}
	
	}

    void CheckAttack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isGrounded)
            {
                anim.SetTrigger("Attack");
            }
        }
    }

	void movePlayer(float horizontal) { 

		//sets the velocity of the player. 
		rb.velocity = new Vector2 (horizontal * playerMovementSpeed, rb.velocity.y);
		//gets the absolute value of the float. 
		anim.SetFloat("speed", Mathf.Abs(horizontal)); 

		//player jump
		if (Input.GetKeyDown (KeyCode.W)) { 
			Debug.Log ("W was pressed");
			
			jumped = true;

			if (isGrounded && jumped) { 

				rb.AddForce (new Vector2 (0, jumpForce));
				isGrounded = false;
                anim.SetTrigger("Jump");
            } else {
				print ("Couldn't jump" + isGrounded + jumped);
			}
		}
        else if (Input.GetKey(KeyCode.S))
        {
            if (isGrounded)
            {
                crawling = true;
                anim.SetBool("Crawl", true);
                col2D.size = new Vector2(.25f, 0.2f);
            }
        }
        else
        {
            if (crawling)
            {
                crawling = false;
                anim.SetBool("Crawl", false);
                col2D.size = new Vector2(.4f, 0.6f);
            }
        }

        anim.SetBool("isGrounded", isGrounded);

	}

	void flipPlayer(float horizontal) { 

		if (!facingRight && horizontal > 0 || facingRight && horizontal < 0) {
			//changes the boolean 
			facingRight = !facingRight; 
			playerScale = transform.localScale;
			//multiples the scale by -1 so that the player flips. 
			playerScale.x *= -1; 
			transform.localScale = playerScale;
		}

	} 

	bool IsGrounded() { 

		// After landing, the Y velocity fluctuates between a bunch of really low numbers for ~1s.
		// Safer to check if it's lower than 0.1 than 0.0
		if (rb.velocity.y <= 0.1) { 
			// for every ground point a new collider is made. 
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, isGround);
				for (int i = 0; i < colliders.Length; i++) { 
					//returns true if the collider is touching something other then the player. 
					if (colliders [i].gameObject != gameObject) { 
						return true;
					} 
				} 
			}
			//returns false if the velocity of the player is greater then 0 . 
		}
		print ("Y velocity > 0");
		return false;
	}

    public void PlayFootstep()
    {
        // TODO Re-enable
        //AudioController.Instance.Footstep();
    }

    public void Equip(bool activate, Item item)
    {

    }

    public void Hold(Item item)
    {

    }

    public void Use(Item item)
    {

    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
        SceneManager.LoadScene("Boss");
    }

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.CompareTag("Monster")) {
            MonsterController monster = coll.gameObject.GetComponent<MonsterController>();
            if (monster.action == MonsterController.Action.ATTACK)
            {
                HP -= 1;
                print("Player HP is" + HP);
            }
            // No animation for getting hurt, so no indication...
            //anim.Play("Hurt");
		}
	}
}
