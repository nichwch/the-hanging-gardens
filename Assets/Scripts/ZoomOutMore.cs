using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutMore : MonoBehaviour {
    
    public GameObject Camera;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Camera.GetComponent<CameraController>().SetZoom(-90);
        }
    }
}
