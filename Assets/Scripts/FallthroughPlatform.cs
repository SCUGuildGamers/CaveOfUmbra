using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class FallthroughPlatform : MonoBehaviour
{
    public PlayerController pc = null;
    public float phaseThroughTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(DisableCollision());
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        Transform child = transform.GetChild(0);
        
        collider.enabled = false;
        child.tag = "NotHazard";

        yield return new WaitForSeconds(phaseThroughTime);
        collider.enabled = true;
        child.tag = "Hazard";
    }
}
