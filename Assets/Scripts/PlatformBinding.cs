using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBinding : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponentInParent<MovingPlatform>().target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            GetComponentInParent<MovingPlatform>().target = null;
    }
}
