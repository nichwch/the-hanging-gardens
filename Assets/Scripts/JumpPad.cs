using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {
    public int jumpPower = 40;
    public GameObject Player;
    public bool touch = false;
    public bool jump = false;
	// Use this for initialization
	void Start () {
		
	}

    //NOTE TO SELF: BUILD ALL JUMP FUNCTIONALITY IN PLAYERCONTROLLER...
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Jump") > 0 && touch &&!jump)
        {
            print("jumpy boi");
            Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            jump = true;
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //print("printy boi");
        if (col.gameObject.name == "Player")
        {
            touch = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        print("exity boi");
        if (col.gameObject.name == "Player")
        {
            touch = false;
            jump = false;
        }
    }
}
