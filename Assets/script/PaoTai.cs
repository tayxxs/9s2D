using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaoTai : MonoBehaviour
{
    public int Hp = 10;
    public GameObject UI;
    public GameObject CMR;
    public float shoottime = 0.2f;
    public float speedq;
    public float rotateTime;
    public float rotateModespeedq;
    public GameObject bullet;
    public bool rotateModeSwitch = false;
    public bool LookAtModeSwitch;
    public bool YaoBaoModeSwitch;
    public bool ModeUp = false;
    public bool onesMode;

    public GameObject[] Box;
    public float LinesLong;
    public float LinesCount;

    float lastTime;
    float curTime;
    float time;
    GameObject Player;
    private LineRenderer[] reader;



    private RaycastHit2D[] hit;
    private Ray2D[] ray;


    enum State
    {
        Clockwise, Anticlockwise
    }
    State mState = State.Clockwise;


    // Use this for initialization
    void Start()
    {
        lastTime = Time.time;
        ray = new Ray2D[4];
        hit = new RaycastHit2D[4];
        reader = GetComponentsInChildren<LineRenderer>();
    }



    // Update is called once per frame
    void Update()
    {
        RaySet();

        if(rotateModeSwitch)
        {
            if(YaoBaoModeSwitch)
            {
                switch(mState)
                {
                    
                    case State.Clockwise:
                        {
                            
                            time += Time.deltaTime;
                            transform.Rotate(Vector3.forward * rotateModespeedq * Time.deltaTime, Space.World);
                            if(time >= rotateTime)
                            {
                                time = 0;
                                mState = State.Anticlockwise;
                                rotateModespeedq = -rotateModespeedq;
                            }
                        }
                        break;
                    case State.Anticlockwise:
                        {
                            time += Time.deltaTime;
                            transform.Rotate(Vector3.forward * rotateModespeedq * Time.deltaTime, Space.World);
                            if (time >= rotateTime)
                            {
                                time = 0;
                                mState = State.Clockwise;
                                rotateModespeedq = -rotateModespeedq;
                            }
                        }
                        break;

                }
            }

            else
            {
                transform.Rotate(Vector3.forward * rotateModespeedq * Time.deltaTime, Space.World);
            }
        }


        if (LookAtModeSwitch && Player != null)
        {
            Vector3 mubiao = Player.transform.position - this.transform.position;
            Quaternion target = Quaternion.LookRotation(Vector3.forward, mubiao);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, speedq);
        }


        if (ModeUp)
        {
            curTime = Time.time;
            if (curTime - lastTime >= shoottime)
            {
                for (int i = 0; i < LinesCount; i++)
                {
                    Instantiate(bullet, this.transform.GetChild(i).position, this.transform.GetChild(i).rotation);
                }

                lastTime = curTime;

            }
        }

        if(onesMode)
        {
            for (int i = 0; i < LinesCount; i++)
            {
                Instantiate(bullet, this.transform.GetChild(i).position, this.transform.GetChild(i).rotation);
            }
            onesMode = false;
        }

        if (Hp <= 0)
        {
            Destroy(this.gameObject);
            if (Box != null)
            {
                for (int i = 0; i < Box.Length; i++)
                    Destroy(Box[i]);
            }
            if (UI != null && CMR != null)
            {
                Instantiate(UI, CMR.transform);
            }

        }

    }



    private void RaySet()
    {

        for (int i = 0; i < LinesCount; i++)
        {
            ray[i] = new Ray2D(transform.position, this.transform.GetChild(i).up * LinesLong);
            hit[i] = Physics2D.Raycast(this.transform.GetChild(i).position, this.transform.GetChild(i).up * LinesLong, LinesLong);
            Debug.DrawRay(this.transform.GetChild(i).position, this.transform.GetChild(i).up * LinesLong, Color.blue);
            reader[i].SetPosition(0, this.transform.GetChild(i).position);
            reader[i].SetPosition(1, hit[i].point);

            if(hit[i].collider!=null)
            {
                if (hit[i].collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {

                    //onesMode = true;
                    ModeUp = true;
                    Player = hit[i].collider.gameObject;
                }
                if(hit[i].collider.gameObject.layer == LayerMask.NameToLayer("NPC"))
                {
                    onesMode = true;
                }
            }

            else
            {
                reader[i].SetPosition(1, this.transform.GetChild(i).position+this.transform.GetChild(i).up * LinesLong);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            Hp--;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("RedBoom"))
        {
            Hp = 0;
        }
    }
}
