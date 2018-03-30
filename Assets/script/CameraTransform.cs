using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour {
    public Vector3 campos;
    public Vector3 rote;
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.layer==LayerMask.NameToLayer("Player"))
        //{
        //    other.gameObject.GetComponent<PlayerControl>().CMR.gameObject.GetComponent<CameraControl>().campos=campos;
        //    other.gameObject.GetComponent<PlayerControl>().CMR.gameObject.GetComponent<CameraControl>().rote = rote;
        //    other.gameObject.GetComponent<PlayerControl>().CMR.gameObject.GetComponent<CameraControl>().speed = speed;
        //}
    }
}
