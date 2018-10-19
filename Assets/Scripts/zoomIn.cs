using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomIn : MonoBehaviour
{

    public GameObject Camera;
    public Transform Player;
    public int zoom;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Camera.GetComponent<CameraController>().SetZoom(zoom);
        }
    }
}
