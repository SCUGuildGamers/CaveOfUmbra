using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerColliderGenerator : MonoBehaviour
{
    // Accessible variables for 'dimension' layer names and difference in Inner Collider Size
    public float InnerColliderDifference = 0.5f;
    public string ColorLayerName = "ColorDim";
    public string GrayLayerName = "GrayDim";

    // Start is called before the first frame update
    void Start()
    {

        // Collects all the gray and color objects
        GameObject[] ColorObjects = GameObject.FindGameObjectsWithTag(ColorLayerName);
        GameObject[] GrayObjects = GameObject.FindGameObjectsWithTag(GrayLayerName);

        // For every single object in the list
        foreach(GameObject obj in GrayObjects)
        {
            // Collects the collider data, sets the InnerCollider to be same size, then adjusts using InnerColliderDifference
            Collider OuterCollider = GetComponent<Collider>();

            Vector3 InnerColliderSize = OuterCollider.bounds.size;
            InnerColliderSize.x -= InnerColliderDifference;
            InnerColliderSize.y -= InnerColliderDifference;

            // Adds the new InnerCollider to the object, sets appropriate values
            BoxCollider2D InnerCollider = obj.AddComponent<BoxCollider2D>();
            InnerCollider.size = InnerColliderSize;
            InnerCollider.isTrigger = true;
        }

        // Repeats thge same for ColorObjects
        foreach(GameObject obj in ColorObjects)
        {
            Collider OuterCollider = GetComponent<Collider>();

            Vector3 InnerColliderSize = OuterCollider.bounds.size;
            InnerColliderSize.x -= InnerColliderDifference;
            InnerColliderSize.y -= InnerColliderDifference;

            BoxCollider2D InnerCollider = obj.AddComponent<BoxCollider2D>();
            InnerCollider.size = InnerColliderSize;
            InnerCollider.isTrigger = true;
        }
    }

    // Update is called once per frame
    /*
    void Update()
    {
        
    }
    */

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"));
        {
            Debug.Log("This thing seems to work...");
        }
    }
}
