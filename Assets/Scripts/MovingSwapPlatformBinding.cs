using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSwapPlatformBinding : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponentInParent<SwapMovingPlatform>().target = collision.gameObject.GetComponent<TarodevController.PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            GetComponentInParent<SwapMovingPlatform>().target = null;
    }
}
