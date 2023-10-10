using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SwitchingMechanic : MonoBehaviour
{
    // Initial variables, can be changed quickly in editor
    public bool color_enabled;
    public bool gray_enabled;
    public int color_layer = 6;
    public int gray_layer = 7;
    // private GameObject[] allObjects;

    void Start()
    {
        // Saving all objects in scene onto an array for later use
        //allObjects = FindObjectsOfType<GameObject>();
        color_enabled = true;
        gray_enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // On SPACE key press
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            SwapDimensions();
        }
    }

    private void SwapDimensions()
    {
        // Go through each GameObject previously gotten
        /*
        foreach (GameObject obj in allObjects)
        {
            // Activate or Deactivate based on the new enabled value
            if (obj.layer == color_layer)
            {
                Physics2D.IgnoreLayerCollision
            }
            else if (obj.layer == gray_layer)
            {
                obj.SetActive(gray_enabled);
            }
        }
        */
        // Update new enabled bool, then either activate or deactivate collision based on new values.
        Debug.Log("We got here");
        color_enabled = !color_enabled;
        gray_enabled = !gray_enabled;
        Physics.IgnoreLayerCollision(0, 6, color_enabled);
        Physics.IgnoreLayerCollision(0, 7, gray_enabled);
    }
}
