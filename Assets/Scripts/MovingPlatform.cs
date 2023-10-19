using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] points = null;
    public bool looping = false;

    private bool isMoving = false;
    private bool isForward = true;

    public float lerpDuration = 3;
    private int cursor = 0;
    private Vector3 startPoint;
    private Vector3 endPoint;

    // Update is called once per frame
    void Update()
    {
        //Prevents movement until lerp is done
        if (isMoving)
            return;
        CalculatePoints();
        StartCoroutine(Lerp(lerpDuration, startPoint, endPoint));
        MoveCursor();
        isMoving = true;
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

    //Moves the cursor and swaps directions if not looping
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

    //Movement of platform using Lerping
    IEnumerator Lerp(float lerpDuration, Vector3 startPoint, Vector3 endPoint)
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            gameObject.transform.position = Vector3.Lerp(startPoint, endPoint, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = endPoint;
        isMoving = false;
    }
}
