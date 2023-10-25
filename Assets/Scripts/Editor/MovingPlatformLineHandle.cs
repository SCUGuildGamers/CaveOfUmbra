using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovingPlatform))]
class MovingPlatformLineHandle : Editor
{
    private void OnSceneGUI()
    {
        MovingPlatform platform = target as MovingPlatform;
        if (platform.points == null ||
            platform.points.Length <= 1)
            return;

        for (int i = 0; i < platform.points.Length - 1; i++)
        {
            Handles.DrawLine(platform.points[i], platform.points[i + 1], 1);
        }

        if (platform.looping)
        {
            Handles.DrawLine(platform.points[platform.points.Length - 1], platform.points[0], 1);
        }
    }
}
