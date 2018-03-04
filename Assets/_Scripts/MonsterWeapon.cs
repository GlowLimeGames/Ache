using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Fake functions, just place the hitbox in front of the player or somewhere far away
    public void Activate()
    {
        gameObject.transform.localPosition = new Vector3(-0.141f, 0.158f, 0f);
    }

    public void Deactivate()
    {
        gameObject.transform.localPosition = new Vector3(0f, -200f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioController.Instance.PlaySFX("player damage sound");
            playerMovement player = other.GetComponent<playerMovement>();
            player.HP -= 1;
            print("Player HP now is " + player.HP);
            Deactivate();
        }
    }
}
