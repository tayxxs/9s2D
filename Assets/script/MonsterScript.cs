using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour {
    public float movespeed;
    public float speedq;
    public GameObject bullet;
    public float shootTime;
    public float LinesLong;
    public int LineCount;
    public float EscMax;

    public float m = 0.05f;//射线密度
    public int Hp;
    public GameObject Ludian;
    public bool FenLieMode;
    public bool Loop;
    public bool canFindNPC;
    public bool Find;
    public GameObject Monster;


    private LineRenderer reader;
    private RaycastHit2D[] hit;
    private Ray2D[] ray;

    GameObject Player;
    float lastTime;
    float curTime;

    bool Noflee;
    bool DaoDa = true;
    int Jb=0;
    Vector3 mubiao;


    enum State
    {
        Go, Back
    }

    State mState = State.Go;

	// Use this for initialization
	void Start () {
        lastTime = Time.time;
        ray = new Ray2D[LineCount];
        hit = new RaycastHit2D[LineCount];
        Player = GameObject.Find("Player2D");
		
	}




	
	// Update is called once per frame
	void Update ()
    {
        RaySet();
        if (Hp <= 0)
        {
            if (FenLieMode)
            {
                if (Monster != null)
                {
                    Instantiate(Monster, this.transform.position + transform.right * 2f, Quaternion.identity);
                    Instantiate(Monster, this.transform.position - transform.right * 2f, Quaternion.identity);
                }
                Destroy(this.gameObject);

            }
            else
            {
                Destroy(this.gameObject);
            }
        }



    }

    private void FixedUpdate()
    {
        if (Find)
        {
            if (Player != null)
            {
                mubiao = Player.transform.position - this.transform.position;
                Follow();
                MonsterAct();
            }
            else
                Find = false;
            

        }
        else
        {
            if (Ludian != null)
            {
                if(Loop)
                {
                    if (Jb >= Ludian.transform.childCount)
                    {
                        Jb = 0;
                    }
                }
                else
                {
                    if (Jb >= Ludian.transform.childCount)
                    {
                        Jb = Ludian.transform.childCount - 2;
                        mState = State.Back;
                    }
                    if (Jb <= 0)
                    {
                        Jb = 0;
                        mState = State.Go;
                    }
                }
                mubiao = Ludian.transform.GetChild(Jb).position - this.transform.position;
                MonsterAct();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BulletMe"))
        {
            Hp--;
            Destroy(other.gameObject);
        }

        if(other.gameObject.layer==LayerMask.NameToLayer("LuDian")&& other.transform.parent.name == Ludian.name)
        {
            Jb = other.transform.GetSiblingIndex();

            switch(mState)
            {
                case State.Go:
                    
                    Jb++;
                    break;
                case State.Back:
                    
                    Jb--;
                    break;
            }
        
        }

    }


    private void RaySet()
    {
        for (int i = 0; i < ray.Length; i++)
        {
            
            if ((i % 2) == 0)
            {
                if(i<35)
                {
                    ray[i] = new Ray2D(transform.position, Vector3.Lerp(this.transform.up, this.transform.right, i * m).normalized * LinesLong);
                    hit[i] = Physics2D.Raycast(transform.position + transform.up, Vector3.Lerp(this.transform.up, this.transform.right, i * m), LinesLong);
                    Debug.DrawRay(transform.position, Vector3.Lerp(this.transform.up, this.transform.right, i * m).normalized * LinesLong, Color.blue);
                }

                else
                {
                    ray[i] = new Ray2D(transform.position, Vector3.Lerp(this.transform.up, this.transform.right, i * m).normalized * LinesLong*0.1f);
                    hit[i] = Physics2D.Raycast(transform.position + transform.up, Vector3.Lerp(this.transform.up, this.transform.right, i * m), LinesLong*0.1f);
                    Debug.DrawRay(transform.position, Vector3.Lerp(this.transform.up, this.transform.right, i * m).normalized * LinesLong*0.1f, Color.yellow);
                }
            }

            if ((i % 2) == 1)
            {
                if(i<=35)
                {
                    ray[i] = new Ray2D(transform.position, Vector3.Lerp(this.transform.up, -this.transform.right, i * m).normalized * LinesLong);
                    hit[i] = Physics2D.Raycast(transform.position + transform.up, Vector3.Lerp(this.transform.up, -this.transform.right, i * m), LinesLong);
                    Debug.DrawRay(transform.position, Vector3.Lerp(this.transform.up, -this.transform.right, i * m).normalized * LinesLong, Color.green);
                }

                else
                {
                    ray[i] = new Ray2D(transform.position, Vector3.Lerp(this.transform.up, -this.transform.right, i * m).normalized * LinesLong*0.1f);
                    hit[i] = Physics2D.Raycast(transform.position + transform.up, Vector3.Lerp(this.transform.up, -this.transform.right, i * m), LinesLong*0.1f);
                    Debug.DrawRay(transform.position, Vector3.Lerp(this.transform.up, -this.transform.right, i * m).normalized * LinesLong*0.1f, Color.yellow);
                }
            }

            if (hit[i].collider != null)
            {
                //Debug.Log(hit[i].collider.gameObject);
                if (hit[i].collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    Find = true;
                    Player = hit[i].collider.gameObject;
                    if (this.transform.parent != null)
                    {
                        MonsterScript[] Monsters;
                        Monsters = this.transform.parent.gameObject.GetComponentsInChildren<MonsterScript>();
                        for (int t = 0; t < Monsters.Length; t++)
                        {
                            if (!Monsters[t].Find)
                                Monsters[t].Find = true;
                        }
                    }
                }

                else if(hit[i].collider.gameObject.layer == LayerMask.NameToLayer("NPC")&&canFindNPC)
                {
                    Find = true;
                    Player = hit[i].collider.gameObject;
                }
            }


         }
    }

    private void Follow()
    {
        curTime = Time.time;


        if (curTime - lastTime >= shootTime)
        {
            Instantiate(bullet, this.transform.position, this.transform.rotation);
            lastTime = curTime;
        }


        if (Player != null)
        {
            Vector3 distance;
            distance = Player.transform.position - transform.position;

            if (distance.magnitude >= EscMax)
            {
                Find = false;
            }
        }
        else
            Find = false;
            

    }

    private void MonsterAct()
    {
        Quaternion target = Quaternion.LookRotation(Vector3.forward, mubiao);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, speedq);
        transform.Translate(Vector3.up * movespeed);
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("RedBoom"))
        {
            Hp = 0;
        }
    }
}


