using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    
    const float DEFAULT_ZOOM = -25;
    public Transform target;
    public PlayerController player;
    public float zoomLevel = DEFAULT_ZOOM;
    public float smoothSpeed = 0.1f;
    private Vector3 velocity = Vector3.zero;

    Vector3 desiredPosition;


    public void FixedUpdate()
    {
        
        if (player.facingRight)
        {
            desiredPosition = target.position + new Vector3(10, 0, zoomLevel);
        }
        if (!player.facingRight)
        {
            desiredPosition = target.position + new Vector3(-10, 0, zoomLevel);
        }
        if(player.myRB.velocity.x<1&&player.myRB.velocity.x > -1&&player.facingRight)
        {
            desiredPosition = target.position + new Vector3(5, 0, zoomLevel);
        }
        if (player.myRB.velocity.x < 1 && player.myRB.velocity.x > -1 && !player.facingRight)
        {
            desiredPosition = target.position + new Vector3(-5, 0, zoomLevel);
        }


        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity,smoothSpeed);
    }
    public void SetZoom(int zoom)
    {
        zoomLevel = zoom;
    }



}

  
