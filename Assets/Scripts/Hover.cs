using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {
    public Transform trans;
    public float amp;
    float initial;


	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
        initial = Random.Range(0,1000);
	}
	
	// Update is called once per frame
	void Update () {
        trans.position = new Vector3(trans.position.x, trans.position.y+(Mathf.Sin(initial+Time.fixedTime*amp)*0.01f), trans.position.z);
	}
}
