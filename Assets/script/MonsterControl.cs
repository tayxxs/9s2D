using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour {
    public float Hp=1;
    public float speed;
    public GameObject bullet;
    public GameObject Player;
    public float shoottime;
    float lastTime;
    float curTime;
    public Vector3 move; 
    Vector3 shoot;
	// Use this for initialization
	void Start () {
        Input.multiTouchEnabled = true;
        move = Vector3.right;
        shoot = Vector3.right;
        Player=GameObject.Find("Player2D");
	}
	
	// Update is called once per frame
    void Update()
    {
        if(Player!=null)
        {
            shoot = Player.transform.position - this.transform.position;
            move.y = Player.transform.position.y - this.transform.position.y;
            move = move.normalized;
        }
        if(Hp<=0)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate ()
    {
         PlayerCtrl(ref move,ref shoot);
    }

    private void PlayerCtrl(ref Vector3 move, ref Vector3 shoot)
    {
        //float y = Camera.main.transform.rotation.eulerAngles.y;
        //shoot = Quaternion.Euler(0, y, 0) * shoot;
        //move = Quaternion.Euler(0, y, 0) * move;

        transform.Translate(move * Time.deltaTime * speed, Space.World);
        if (shoot != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, shoot);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);
            curTime = Time.time;
            if (curTime - lastTime >= shoottime)
            {
                Instantiate(bullet, this.transform.position + transform.forward, this.transform.rotation);
                lastTime = curTime;
            }
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            float shootx, shooty;
            shootx = other.transform.position.x - this.transform.position.x;
           
            shooty = other.transform.position.y - this.transform.position.y;
            shoot = new Vector3(shootx,shooty,0);
        }


    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BulletMe"))
        {
            ChangeMove(other);
            Hp--;
            Debug.Log(1);
        }
    }

    private void ChangeMove(Collider2D other)
    {
        float movex, movez;
        movex = this.transform.position.x - other.transform.position.x;
        if (movex > 0)
            movex = 1f;
        else if (movex < 0)
            movex = -1f;
        else
            movex = 0;
        movez = 1;
        move = new Vector3(movex, 0, movez);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BulletMe"))
        {
            move = new Vector3(1, 0, 0);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            shoot = Vector3.right;
        }
    }




}
