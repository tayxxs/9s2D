using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBox : MonoBehaviour {
    public int Hp;
    public GameObject Ctrl;
    public GameObject BlackCore;
    public bool NPC;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Hp <= 0)
        {
            if(NPC)
            {
                Instantiate(BlackCore, this.transform.position + transform.forward, this.transform.rotation);
            }
            if(Ctrl!=null)
            {
                Ctrl.SendMessage("Isnull");
            }
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("BulletYou")|| other.gameObject.layer == LayerMask.NameToLayer("BulletMe"))
        {
            Hp--;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("RedBoom"))
        {
            Hp = 0;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("RedBoom"))
        {
            Hp = 0;
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!NPC)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("BulletYou"))
            {
                Hp--;
            }
        }

    }
}
