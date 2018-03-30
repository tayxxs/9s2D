using UnityEngine;
using System.Collections;

public class Plank : MonoBehaviour
{
    enum State
    {
        Go, Back
    }
    State mState = State.Go;

    [Header("The time move from point1 to point2"), Range(0.1f, 10f)]
    public float moveTime = 1f;

    [Header("Two points of the moveway")]
    public Transform[] points = new Transform[2];

    public bool ModeUp;
    Transform mTransform = null;
    private Vector3 point1;
    private Vector3 point2;
    void Start()
    {
        mTransform = transform;
        point1 = points[0].position;
        point2= points[1].position;
        mTransform.position = point1;
    }

    float movePercent = 0;
    void Update()
    {
        if(ModeUp)
        {
            switch (mState)
            {
                case State.Go:
                    {
                        movePercent += Time.deltaTime / moveTime;
                        if (movePercent >= 1)
                            movePercent = 1;
                        mTransform.position = Vector3.Lerp(point1, point2, movePercent);
                        if (movePercent == 1)
                        {
                            movePercent = 0;
                            mState = State.Back;
                        }
                    }
                    break;
                case State.Back:
                    {
                        movePercent += Time.deltaTime / moveTime;
                        if (movePercent >= 1)
                            movePercent = 1;
                        mTransform.position = Vector3.Lerp(point2, point1, movePercent);
                        if (movePercent == 1)
                        {
                            movePercent = 0;
                            mState = State.Go;
                        }
                    }
                    break;
            }
        }


    }
}
