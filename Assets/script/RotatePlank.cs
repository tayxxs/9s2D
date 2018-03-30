using UnityEngine;
using System.Collections;

public class RotatePlank : MonoBehaviour
{
    public float rotateSpeed1;
    public float rotateSpeed2;
    [Header("The time of unidirectional rotation"), Range(0.1f, 100f)]
    public float rotateTime = 0.5f;
    public float idleTime;
    public bool isDoubleDirection = true;

    enum State
    {
        Clockwise, Anticlockwise,idle
    }
    State mState = State.idle;
    State lState = State.Anticlockwise;

    Vector3 eulerAngles;
    Transform mTransform;
    void Start()
    {
        mTransform = transform;
        eulerAngles = transform.localRotation.eulerAngles;
    }

    float time;
    void FixedUpdate()
    {
        switch (mState)
        {
            case State.Clockwise:
                {
                    if (isDoubleDirection)
                        time += Time.fixedDeltaTime;
                    
                    transform.Rotate(Vector3.forward,rotateSpeed1*Time.fixedDeltaTime);

                    if (time >= rotateTime)
                    {
                        time = 0;
                        lState = State.Clockwise;
                        mState = State.idle;
                    }
                }
                break;
            case State.idle:
                {
                    time += Time.deltaTime;
                    if (time >= idleTime)
                    {
                        time = 0;
                        if (lState == State.Clockwise)
                            mState = State.Anticlockwise;
                        else
                            mState = State.Clockwise;      
                    }
                }
                break;
            case State.Anticlockwise:
                {
                    if (isDoubleDirection)
                        time += Time.deltaTime;

                    transform.Rotate(Vector3.forward, -rotateSpeed2 * Time.fixedDeltaTime);

                    if (time >= rotateTime)
                    {
                        time = 0;
                        lState = State.Anticlockwise;
                        mState = State.idle;
                    }
                }
                break;
        }

    }
}
