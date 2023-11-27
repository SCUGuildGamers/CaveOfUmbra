using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class FallthroughMechanic : MonoBehaviour
{
    public PlayerController pc = null;
    public GameObject otherChild = null;
    public GameObject parentObject = null;
    // contact check makes sure that code only activates when the player is in contact with the relevant platform
    private bool contactCheck;
    
    // Start is called before the first frame update
    void Start()
    {
        // At start, save the parent Object that the thing is attached to, as well as inherit the PlayerController and the other child "InnerBoxCollider"
        parentObject = transform.parent.gameObject;
        FallthroughPlatformGenerator parentScript = parentObject.GetComponent<FallthroughPlatformGenerator>();
        pc = parentScript.pc;
        otherChild = parentScript.otherChild;

        // Start with contact being false
        contactCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If contact is being made, and the player is pressing down and is on a platform, then begin falling mechanic
        if((contactCheck == true) && (Input.GetKeyDown(KeyCode.S)) && (pc._colDown == true))
        {
            // Changes tag, and disables both the inner collider for insta death as well as the actual block itself
            otherChild.tag = "NotHazard";
            otherChild.GetComponent<BoxCollider2D>().enabled = false;
            parentObject.GetComponent<BoxCollider2D>().enabled = false;

        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Simple code on collision that checks if contact is being made
        if(otherCollider.tag == "Player")
        {
            contactCheck = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D otherCollider)
    {
        // This should actiavte everytime the player stops making contact with the specific block, just resets everything back to the way it should be
        // Actually this is wrong and needs to check the layer that the thing is on before it actiavtes blindly
        if(contactCheck == true && otherCollider.tag == "Player")
        // Assumes that all other checks are unnessary at this point
        {
            otherChild.tag = "Hazard";
            parentObject.GetComponent<BoxCollider2D>().enabled = true;
            otherChild.GetComponent<BoxCollider2D>().enabled = true;
            contactCheck = false;
            Debug.Log("Exit Triggered");
        }
        
    }  
}
