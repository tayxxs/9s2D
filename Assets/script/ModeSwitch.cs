using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitch : MonoBehaviour {
    public GameObject[] JiGuan;
    public bool fireMode;
    public bool actUp=true;
    PaoTai[] b;
    Plank[] p;
    MonsterNest[] m;
    bool wake = false;
    bool ModeUp;
    bool RE;

	// Use this for initialization
	void Start ()
    {

        b = new PaoTai[JiGuan.Length];
        p=new Plank[JiGuan.Length];
        m = new MonsterNest[JiGuan.Length];
        for (int i = 0; i < JiGuan.Length; i++)
        {
            b[i] = JiGuan[i].GetComponent<PaoTai>();
            p[i]=JiGuan[i].GetComponent<Plank>();
            m[i] = JiGuan[i].GetComponent<MonsterNest>();

        }

        if (transform.childCount > 0)
            RE = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0 && RE)
        {
            ModeUp = true;
        }

        if (ModeUp)
        {
            if (!wake)
            {
                for (int i = 0; i < JiGuan.Length; i++)
                {
                    if (b[i] != null)
                        b[i].ModeUp = true;//炮台射击
                    if (p[i] != null)
                        p[i].ModeUp = true;//炮台旋转
                    if (m[i] != null)
                        m[i].ModeUp = true;//开启出怪
                }
                wake = true;
            }
        }

        if (!actUp)
        {
            if (!wake)
            {
                for (int i = 0; i < JiGuan.Length; i++)
                {
                    JiGuan[i].GetComponent<LuXian>().Ludian = null;
                }
                wake = true;
            }
        }
            

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
                actUp = false;
                //ModeUp = true;禁止开关开启机关模式
            if(fireMode)
            {
                other.gameObject.GetComponent<PlayerControl>().canFire=true;

            }

        }
    }
}
