using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class FallthroughPlatformGenerator : MonoBehaviour
{
    public PlayerController pc = null;
    public float phaseThroughTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Get Collider from the object script is attached to
        BoxCollider2D ObjectCollider = gameObject.GetComponent<BoxCollider2D>();

        // Save the size of said collider to ColliderSize variable
        Vector3 ColliderSize = ObjectCollider.bounds.size;

        // Create a new gameobject
        GameObject FallObject = new GameObject("FallBounds");

        // Make CrumbleObject child of original game object, set position to local 0
        FallObject.transform.parent = gameObject.transform;
        FallObject.transform.localPosition = Vector3.zero;

        // Create new BoxCollider2D
        BoxCollider2D FallCollider = FallObject.AddComponent<BoxCollider2D>();
        FallCollider.size = ColliderSize;
        FallCollider.isTrigger = true; 

        FallObject.AddComponent<FallthroughMechanic>();
        }

    // Update is called once per frame
    void Update()
    {
        // need to check if grounded through player movement
        if(Input.GetKeyDown(KeyCode.S) && pc._colDown == true)
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

/*public class FallthroughChild : MonoBehaviour
{
    OnCollisionEnter
}

*/