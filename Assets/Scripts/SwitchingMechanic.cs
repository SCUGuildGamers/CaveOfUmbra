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
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material transparentMaterial;
    public PlayerController pc = null;
    private GameObject[] allObjects;
    private bool _lock = false;

    void Start()
    {
        // Saving all objects in scene onto an array for later use
        allObjects = FindObjectsOfType<GameObject>();
        SwapDimensions();
    }

    // Update is called once per frame
    void Update()
    {
        // makes sure swap doesn't happen multiple times per jump
        // learned this in Operating Systems!
        if (!pc.JumpingThisFrame)
            _lock = false;
        // When player jumps
        if(!_lock && pc.JumpingThisFrame)
        {
            _lock = true;
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
                Swap(obj, color_enabled);
            }
            else if (Mathf.Pow(2, obj.layer) == grayLayer.value)
            {
                Swap(obj, gray_enabled);
            }
        }

        // Adds the color layer to the player's list of collidable layers
        if (color_enabled)
            pc.groundLayer |= (1 << (int)Mathf.Log(colorLayer.value, 2));
        // Removes the color layer from the player's list of collidable layers
        else
            pc.groundLayer &= ~(1 << (int)Mathf.Log(colorLayer.value, 2));
        if (gray_enabled) 
            pc.groundLayer |= (1 << (int)Mathf.Log(grayLayer.value, 2));
        else
            pc.groundLayer &= ~(1 << (int)Mathf.Log(grayLayer.value, 2));
    }

    private void Swap(GameObject obj, bool enabled)
    {
        //Swaps material
        obj.GetComponent<Renderer>().material = enabled ? defaultMaterial : transparentMaterial;

        //Prevents hazards from killing player when not in same dimension
        if (obj.tag == "Hazard")
            obj.tag = "NotHazard";
        else if (obj.tag == "NotHazard")
            obj.tag = "Hazard";

        //Removes the inner kill collider from objects when not in same dimension as player
        Transform innerCollider = obj.transform.Find("InnerBoxCollider");
        if (innerCollider != null)
            innerCollider.gameObject.SetActive(enabled);

        //Stops moving platforms from moving player when not in same dimension
        PlatformBinding pb = obj.GetComponentInChildren<PlatformBinding>(true);
        if (pb != null)
            pb.gameObject.SetActive(enabled);
    }
}
