using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endingConsole : MonoBehaviour
{

    public GameObject ContextMenu;
    public GameObject TitanEye;
    public Text ContextText;
    public GameObject Player;
    GameObject[] platforms;
    bool contact = false;
    bool processed = false;

    private void Start()
    {
        TitanEye.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Interact") > 0 && contact && !processed)
        {
            StartCoroutine(MoveForward());
            processed = true;

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            ContextMenu.SetActive(true);
            ContextText.text = "Press x to retrive the Titan's eye";
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

    IEnumerator MoveForward()
    {
        TitanEye.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        while(TitanEye.transform.position.z>25)
        {
            TitanEye.transform.position = new Vector3(TitanEye.transform.position.x, TitanEye.transform.position.y, TitanEye.transform.position.z - 0.1f);
            yield return new WaitForFixedUpdate();
        }
        TitanEye.GetComponent<CircleCollider2D>().enabled = true;

        yield break;
    }

}

