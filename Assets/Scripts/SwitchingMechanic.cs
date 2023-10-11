using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;



public class SwitchingMechanic : MonoBehaviour
{
    // Initial variables, can be changed quickly in editor
    public bool color_enabled = true;
    public bool gray_enabled = false;
    [SerializeField] private LayerMask colorLayer;
    [SerializeField] private LayerMask grayLayer;
    public PlayerController pc = null;
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
        // When player jumps
        if(pc.JumpingThisFrame)
        {
            SwapColorValues();
            SwapDimensions();
        }
    }

    private void SwapColorValues()
    {
        // Update new enabled bool
        color_enabled = !color_enabled;
        gray_enabled = !gray_enabled;
    }

    private void SwapDimensions()
    {
        // Go through each GameObject previously gotten
        foreach (GameObject obj in allObjects)
        {
            // Activate or Deactivate based on the new enabled value
            if (Mathf.Pow(2, obj.layer) == colorLayer.value)
            {
                obj.SetActive(color_enabled);
            }
            else if (Mathf.Pow(2, obj.layer) == grayLayer.value)
            {
                obj.SetActive(gray_enabled);
            }
        }

        if (color_enabled)
            pc.groundLayer |= (1 << (int)Mathf.Log(colorLayer.value, 2));
        else
            pc.groundLayer &= ~(1 << (int)Mathf.Log(colorLayer.value, 2));
        if (gray_enabled) 
            pc.groundLayer |= (1 << (int)Mathf.Log(grayLayer.value, 2));
        else
            pc.groundLayer &= ~(1 << (int)Mathf.Log(grayLayer.value, 2));
    }
}
