using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrithPoint : MonoBehaviour {

    public GameObject[] Monster;

    //List<GameObject> rubbishBags = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < Monster.Length; i++)
        {
            Monster[i].SetActive(true);
            Destroy(this);
        }
      
    }

}
