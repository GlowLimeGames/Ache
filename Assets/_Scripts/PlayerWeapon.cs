using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Fake functions, just place the hitbox in front of the player or somewhere far away
    public void Activate()
    {
        gameObject.transform.localPosition = new Vector3(0.131f, 0.208f, 0f);
    }

    public void Deactivate()
    {
        gameObject.transform.localPosition = new Vector3(0f, -100f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {
            MonsterController monster = other.GetComponent<MonsterController>();
            monster.HP -= 1;
            print("Monster HP now is " + monster.HP);
            //monster.action = MonsterController.Action.DELAY;
            //monster.anim.Play("Idle");
            Deactivate();
        }
    }
}
