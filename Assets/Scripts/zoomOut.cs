using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomOut : MonoBehaviour {
    
    public GameObject Camera;
    public Transform Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.position.y<-88)
        {
            Camera.GetComponent<CameraController>().SetZoom(-25);
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Camera.GetComponent<CameraController>().SetZoom(-50);
        }
    }
}
