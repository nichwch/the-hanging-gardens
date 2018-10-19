using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PerspectiveDarken : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float z = GetComponent<Transform>().position.z;
        float colorcode = ((300 - z) / 300);
        GetComponent<SpriteRenderer>().color = new Color(colorcode, colorcode, colorcode);

    }
}


