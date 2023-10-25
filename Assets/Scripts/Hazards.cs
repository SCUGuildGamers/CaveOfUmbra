using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    private PlayerPosition PlayerPositionScript;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        PlayerPositionScript = playerObject.GetComponent<PlayerPosition>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            other.transform.position = PlayerPositionScript.initialPosition;
            Debug.Log("Player triggered a hazard.");
        }
    }
}
