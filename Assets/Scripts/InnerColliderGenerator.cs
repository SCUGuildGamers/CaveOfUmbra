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
    public string InnerCollisionTag = "InstantDeath";

    // Start is called before the first frame update
    void Start()
    {
        // Made some mad edits with Linq 
        // Collects all the gray and color objects
        GameObject[] ColorObjects = GameObject.Find(SceneManager.GetActiveScene().name).Descendants().Where(t=>t.layer == LayerMask.NameToLayer(layerName)).ToList();
        GameObject[] GrayObjects = FindObjectsOfType<GameObject>().Where(obj => obj.layer == LayerMask.NameToLayer(GrayLayerName)).ToArray();

        Debug.Log(ColorObjects);

        // For every single object in the list
        foreach(GameObject obj in GrayObjects)
        {
            // Collects the collider data, sets the InnerCollider data to be same size, then adjusts using InnerColliderDifference
            Collider OuterCollider = GetComponent<Collider>();

            Vector3 InnerColliderSize = OuterCollider.bounds.size;
            InnerColliderSize.x -= InnerColliderDifference;
            InnerColliderSize.y -= InnerColliderDifference;

            // Creating a child to the Object onto which we attach a BoxCollider2D
            GameObject innerColliderObject = new GameObject("InnerBoxCollider");
            innerColliderObject.transform.parent = obj.transform;

            // Adds the new InnerCollider to the object, sets appropriate values
            BoxCollider2D InnerCollider = innerColliderObject.AddComponent<BoxCollider2D>();
            InnerCollider.size = InnerColliderSize;
            InnerCollider.isTrigger = true;

            // Set the inner Objects tag to something for the sake of collision later on
            innerColliderObject.tag = "InstantDeath";
        }

        // Repeats thge same for ColorObjects
        foreach(GameObject obj in ColorObjects)
        {
            Collider OuterCollider = GetComponent<Collider>();

            Vector3 InnerColliderSize = OuterCollider.bounds.size;
            InnerColliderSize.x -= InnerColliderDifference;
            InnerColliderSize.y -= InnerColliderDifference;

            GameObject innerColliderObject = new GameObject("InnerBoxCollider");
            innerColliderObject.transform.parent = obj.transform;

            BoxCollider2D InnerCollider = obj.AddComponent<BoxCollider2D>();
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
