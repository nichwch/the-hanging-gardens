using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    public Rigidbody2D Player;
    public string[] Responses;
    SpriteRenderer myRenderer;

	// Use this for initialization
	void Start () {
        myRenderer = GetComponent<SpriteRenderer>();
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.position.x>transform.position.x)
        {
            myRenderer.flipX = true;
        }
        if (Player.position.x < transform.position.x)
        {
            myRenderer.flipX = false;
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled=true;
            transform.GetChild(0).GetComponent<TextMesh>().text = Responses[Random.Range(0,3)];
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }

    }
}
