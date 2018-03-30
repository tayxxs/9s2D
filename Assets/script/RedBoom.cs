using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBoom : MonoBehaviour {
    public int Hp;
    float t;
    public Vector3 oldScale = new Vector3(0.1f, 0.1f, 0.1f);
    public bool BoomSwitch;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(BoomSwitch)
        {
            if (Hp <= 0)
            {

                transform.localScale = Vector3.Slerp(oldScale, oldScale * 3f, t += 0.03f);
            }

            if (transform.localScale == oldScale * 3f)
            {
                Destroy(this.gameObject);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BulletMe")||other.gameObject.layer == LayerMask.NameToLayer("BulletYou"))
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
}
