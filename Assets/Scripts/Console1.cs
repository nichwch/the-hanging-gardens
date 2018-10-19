using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console1 : MonoBehaviour {
    
    public GameObject ContextMenu;
    public GameObject Bullet;
    public Transform platform;
    public Text ContextText;
    public GameObject Player;
    GameObject[] platforms;
    bool contact = false;
    bool processed = false;

	// Use this for initialization
	void Start () {
        platforms = GameObject.FindGameObjectsWithTag("Level 2 Platform");
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].GetComponent<BoxCollider2D>().enabled = false;
        }
	}

    private void FixedUpdate()
    {
        if (Input.GetAxis("Interact") > 0 &&contact&&!processed)
        {
            if(Player.transform.position.z<5)
            {
                platforms = GameObject.FindGameObjectsWithTag("Level 1 Platform");
                for (int i = 0; i < platforms.Length; i++)
                {
                    platforms[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                StartCoroutine(MoveBack());
                processed = true;
            }
            if (Player.transform.position.z>20)
            {
                platforms = GameObject.FindGameObjectsWithTag("Level 2 Platform");
                for (int i = 0; i < platforms.Length; i++)
                {
                    platforms[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                StartCoroutine(MoveForward());
                processed = true;
            }

        } 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            ContextMenu.SetActive(true);
            ContextText.text = "Press x to activate platform";
            contact = true;
        }


    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            ContextMenu.SetActive(false);
            contact = false;
        }

    }

    IEnumerator MoveBack()
    {
        Player.GetComponent<SpriteRenderer>().sortingOrder = -40;
        Bullet.GetComponent<SpriteRenderer>().sortingOrder = -40;
        while(transform.position.z<25)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
            platform.position = new Vector3(platform.position.x, platform.position.y, platform.position.z + 0.1f);
            Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z + 0.1f);
            yield return new WaitForFixedUpdate();
        }
        platforms = GameObject.FindGameObjectsWithTag("Level 2 Platform");
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].GetComponent<BoxCollider2D>().enabled = true;
        }
        processed = false;
        yield break;
    }

    IEnumerator MoveForward()
    {
        
        while (transform.position.z > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
            platform.position = new Vector3(platform.position.x, platform.position.y, platform.position.z - 0.1f);
            Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z - 0.1f);
            yield return new WaitForFixedUpdate();
        }
        platforms = GameObject.FindGameObjectsWithTag("Level 1 Platform");
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].GetComponent<BoxCollider2D>().enabled = true;
        }
        Player.GetComponent<SpriteRenderer>().sortingOrder = 0;
        Bullet.GetComponent<SpriteRenderer>().sortingOrder = 4;
        processed = false;
        yield break;
    }
}
