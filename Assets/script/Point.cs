using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour {
    public float time = 0;
    public Camera cam;
    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = time.ToString("0.00");
	}

    private void FixedUpdate()
    {
        if (time >= 0)
            time += Time.deltaTime;
    }

        
}

