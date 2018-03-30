using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuXian : MonoBehaviour {

    public float speedq;
    public float movespeed;
    public GameObject Ludian;
    public bool Loop;
    public bool ZhuanXiang;
    int Jb = 0;
    Vector3 mubiao;

    enum State
    {
        Go, Back
    }

    State mState = State.Go;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Ludian != null)
        {
            if (Loop)
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
            mubiao = mubiao.normalized;
            if(ZhuanXiang)
            {
                Quaternion target = Quaternion.LookRotation(Vector3.forward, mubiao);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, speedq);
                transform.Translate(Vector3.up * movespeed);
            }
            else
            {
                transform.Translate(mubiao * movespeed);
            }

        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("LuDian") && other.transform.parent.name == Ludian.name)
        {
            Jb = other.transform.GetSiblingIndex();
            switch (mState)
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
}
