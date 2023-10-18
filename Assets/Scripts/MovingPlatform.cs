using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] points = null;
    public bool looping = false;
    private bool isMoving = false;

    public float lerpDuration = 3;
    private float startValue;
    private float endValue;
    private float valueToLerp;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
            return;

    }
}
