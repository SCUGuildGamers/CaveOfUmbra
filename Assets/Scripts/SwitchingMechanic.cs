using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SwitchingMechanic : MonoBehaviour
{
    // Initial variables, can be changed quickly in editor
    public bool color_enabled = true;
    public bool gray_enabled = false;
    public int color_layer = 6;
    public int gray_layer = 7;
    private GameObject[] allObjects;

    void Start()
    {
        // Saving all objects in scene onto an array for later use
        allObjects = FindObjectsOfType<GameObject>();
        SwapDimensions();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Switching initated");
        // On SPACE key press
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwapDimensions();
        }
    }

    private void SwapDimensions()
    {
        // Go through each GameObject previously gotten
        foreach (GameObject obj in allObjects)
        {
            // Activate or Deactivate based on the new enabled value
            if (obj.layer == color_layer)
            {
                obj.SetActive(color_enabled);
            }
            else if (obj.layer == gray_layer)
            {
                obj.SetActive(gray_enabled);
            }
        }
    }
}
