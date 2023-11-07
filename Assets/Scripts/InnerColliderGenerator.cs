using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using UnityEngine.SceneManagement;

public class InnerColliderGenerator : MonoBehaviour
{
    // Accessible variables for 'dimension' layer names and difference in Inner Collider Size
    public float InnerColliderDifference = 0.5f;
    public string ColorLayerName = "ColorDim";
    public string GrayLayerName = "GrayDim";
    public string InnerCollisionTag = "Hazard";
    
    // Start is called before the first frame update
    void Start()
    {
        // Made some mad edits with Linq 
        // Collects all the gray and color objects
        GameObject[] ColorObjects = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.layer == LayerMask.NameToLayer(ColorLayerName)).ToArray();
        GameObject[] GrayObjects = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.layer == LayerMask.NameToLayer(GrayLayerName)).ToArray();

        // For every single object in the list
        foreach(GameObject obj in GrayObjects)
        {
            // Collects the collider data, sets the InnerCollider data to be same size, then adjusts using InnerColliderDifference
            BoxCollider2D OuterCollider = obj.GetComponent<BoxCollider2D>();

            Vector3 InnerColliderSize = OuterCollider.bounds.size;
            InnerColliderSize.x -= InnerColliderDifference;
            InnerColliderSize.y -= InnerColliderDifference;

            // Creating a child to the Object onto which we attach a BoxCollider2D
            GameObject innerColliderObject = new GameObject("InnerBoxCollider");
            innerColliderObject.transform.parent = obj.transform;
            innerColliderObject.transform.localPosition = Vector3.zero;

            // Adds the new InnerCollider to the object, sets appropriate values
            BoxCollider2D InnerCollider = innerColliderObject.AddComponent<BoxCollider2D>();
            InnerCollider.size = InnerColliderSize;
            InnerCollider.isTrigger = true;

            // Set the inner Objects tag to something for the sake of collision later on
            innerColliderObject.tag = InnerCollisionTag;
        }

        // Repeats thge same for ColorObjects
        foreach(GameObject obj in ColorObjects)
        {
            BoxCollider2D OuterCollider = obj.GetComponent<BoxCollider2D>();

            Vector3 InnerColliderSize = OuterCollider.bounds.size;
            InnerColliderSize.x -= InnerColliderDifference;
            InnerColliderSize.y -= InnerColliderDifference;

            GameObject innerColliderObject = new GameObject("InnerBoxCollider");
            innerColliderObject.transform.parent = obj.transform;
            innerColliderObject.transform.localPosition = Vector3.zero;

            BoxCollider2D InnerCollider = innerColliderObject.AddComponent<BoxCollider2D>();
            InnerCollider.size = InnerColliderSize;
            InnerCollider.isTrigger = true;

            innerColliderObject.tag = InnerCollisionTag;
        }
    }
    // Update is called once per frame
    /*
    void Update()
    {
        
    }
    */
}
