using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNest : MonoBehaviour
{

    public float BrithTime;
    public GameObject[] Monster;
    public float Hp;
    float curTime;
    float lastTime;
    int L;
    public bool ModeUp=false;
    int i = 0;
	// Use this for initialization
	void Start () {
        lastTime = Time.time;
        L = Monster.Length;

	}
	
	// Update is called once per frame
	void Update () {
        
        if (Hp <= 0||L<=0)
        {
            Destroy(this.gameObject);
        }

        if (L > 0&&ModeUp)
        {
            curTime = Time.time;

            if (curTime - lastTime >= BrithTime)
            {
                
                if (Monster[i] != null)
                {
                    Instantiate(Monster[i], this.transform.position + transform.forward * 2f, this.transform.rotation);
                }
                i++;
                lastTime = curTime;
                L--;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        

        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ModeUp = true;
        }



    }
}
