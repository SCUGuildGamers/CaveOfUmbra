using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMovingPlatform : MonoBehaviour
{
    public Vector3[] points = null;
    public bool looping = false;

    private bool isMoving = false;
    public bool isForward = true; // change this back to private later

    public float lerpDuration = 3;
    public int cursor = 0; // change this back to private later
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 _offset, _previousPosition;
    private IEnumerator stopLerp;
    public TarodevController.PlayerController target;

    public GameObject pc;

    // Update is called once per frame

    public bool MovingEquals = false;
    private Vector3 tempPosition;
    private Vector3 tempyPosition;
    private bool _lock = false;
    void Update()
    {
        //Prevents movement until lerp is done
        if (isMoving)
            return;
        CalculatePoints();
        StartCoroutine(stopLerp = Lerp(lerpDuration, startPoint, endPoint));
        MoveCursor();
        isMoving = true;

        if (!pc.GetComponent<TarodevController.PlayerController>().JumpingThisFrame)
            _lock = false;
        // When player jumps
       


    }
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("is this going off");
            _lock = true;
            if (!MovingEquals)
            {
                Debug.Log("stop the platform");
                FlipActive();
                MovingEquals = true;
            }
            else
            {
                Debug.Log("move the platform");
                MovingEquals = false;
                FlipToRightDirection();
            }

        }
    }

    void Start()
    {
        //StartCoroutine(stopCouroutine());
        //StartCoroutine(startTheCoroutine());
    }

    //Selects the current cursor point and the corresponding destination
    private void CalculatePoints()
    {

        startPoint = points[cursor];
        if (looping)
            endPoint = points[(cursor + 1) % points.Length];
        else
            //Will select the point in front or behind depending on direction of lerp
            endPoint = points[cursor + (isForward ? 1 : -1)];
    }

    //Moves the cursor and MovingEqualss directions if not looping
    private void MoveCursor()
    {
        if (looping)
        {
            cursor = (cursor + 1) % points.Length;
            return;
        }
        else
        {
            if (isForward)
            {
                cursor++;
                if (cursor == points.Length - 1)
                    isForward = false;
            }
            else
            {
                cursor--;
                if (cursor == 0)
                    isForward = true;
            }
        }
    }

    private void stopLerping()
    {
        tempPosition = transform.position;
        StopCoroutine(stopLerp);
    }

    //Movement of platform using Lerping
    IEnumerator Lerp(float lerpDuration, Vector3 startPoint, Vector3 endPoint)
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            _previousPosition = gameObject.transform.position;
            gameObject.transform.position = Vector3.Lerp(startPoint, endPoint, timeElapsed / lerpDuration);
            _offset = gameObject.transform.position - _previousPosition;
            if (target != null)
            {
                if (target.colLeft)
                    _offset.x = Mathf.Clamp(_offset.x, 0f, Mathf.Infinity);
                if (target.colRight)
                    _offset.x = Mathf.Clamp(_offset.x, Mathf.NegativeInfinity, 0f);
                if (!target.Grounded)
                    _offset.y = 0f;
                target.transform.position += _offset;
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = endPoint;
        isMoving = false;
    }

    IEnumerator stopCouroutine()
    {
        Debug.Log("stopping the coroutines");
        yield return new WaitForSeconds(5f);
        tempPosition = transform.position;
        tempyPosition = endPoint;
        StopCoroutine(stopLerp);
        Debug.Log("starting the moving platform again in 5 seconds");
        yield return new WaitForSeconds(5f);

        //CalculatePoints();
        
        
        //MoveCursor();
    }

    // calculate the points 

    private void FlipActive()
    {
        tempPosition = transform.position;
        tempyPosition = endPoint;
        StopCoroutine(stopLerp);
    }
    private void FlipToRightDirection()
    {
        float stateDist;
        float baseDist;

        stateDist = Vector3.Distance(transform.position, endPoint);
        baseDist = Vector3.Distance(startPoint, endPoint);
        float newLerpDuration = lerpDuration * (stateDist / baseDist);
        StartCoroutine(stopLerp = Lerp(newLerpDuration, tempPosition, tempyPosition));

    }
    
}


