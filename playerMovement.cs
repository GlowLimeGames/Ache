using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	Rigidbody2D rb; 
	public float playerMovementSpeed; 
	bool facingRight; 
	Vector3 playerScale; 
	Animator anim; 
	public Transform[] groundPoints; 
	public float groundRadius, jumpForce;  
	LayerMask isGround;
	bool isGrounded, jumped;
	public float horizontal;

	// Use this for initialization
	void Start () {

		jumpForce = 2;
		isGrounded = IsGrounded (); 
		groundRadius = .1f;
		facingRight = true; 
		rb = GetComponent <Rigidbody2D> (); 
		playerMovementSpeed = 3; 
		anim = GetComponent < Animator> (); 

	}

	// Update is called once per frame
	void Update () {

		horizontal = Input.GetAxis ("Horizontal"); 
		movePlayer (horizontal); 
		flipPlayer (horizontal); 

	}

	void movePlayer(float horizontal) { 

		//sets the velocity of the player. 
		rb.velocity = new Vector2 (horizontal * playerMovementSpeed, rb.velocity.y);
		//gets the absolute value of the float. 
		anim.SetFloat("speed", Mathf.Abs(horizontal)); 

		//player jump
		if (Input.GetKeyDown (KeyCode.W)) { 

			jumped = true; 

			if (isGrounded && jumped) { 

				isGrounded = false; 
				rb.AddForce (new Vector2 (0, jumpForce)); 

			}
		}

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
		
		if (rb.velocity.y <= 0) { 
			// for ever ground point a new collider is made. 
			foreach (Transform point in groundPoints) { 
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, isGround);

				for (int i = 0; i < colliders.Length; i++) { 
					// returns true if the collider is touching something other then the player. 
					if (colliders [i].gameObject != gameObject) { 
						return true;
					} 
				}
			}
		}
		//returns false if the velocity of the player is greater then 0 . 
		return false; 
	}
}
