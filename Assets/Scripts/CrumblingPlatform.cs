using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    public float timeTillCrumble = 1f;
    public float timeTillReturn = 3f;
    public bool crumble_enabled = true;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material crumbleMaterial;
    private BoxCollider2D innerCollider;
    private BoxCollider2D outerCollider;


    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D ObjectCollider = gameObject.GetComponent<BoxCollider2D>();

        Vector3 ColliderSize = ObjectCollider.bounds.size;

        GameObject CrumbleObject = new GameObject("CrumblingBounds");

        CrumbleObject.transform.parent = gameObject.transform;
        CrumbleObject.transform.localPosition = Vector3.zero;

        BoxCollider2D CrumbleCollider = CrumbleObject.AddComponent<BoxCollider2D>();
        CrumbleCollider.size = ColliderSize;
        CrumbleCollider.isTrigger = true; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Whenever the player hitbox interacts with the object hitbox, start coroutine
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCorouti
        }
    }

    // Any special affects that need to be added can be done here in the crumble
    IEnumerator Crumble()
    {
        // Get the child of the gameobject, then get the collider of both main game object and child's
        Transform child = transform.GetChild(0);
        innerCollider = child.GetComponent<BoxCollider2D>();
        outerCollider = gameObject.GetComponent<BoxCollider2D>();

        // Wait until the designated time, disable appropriate colliders and use material switch
        yield return new WaitForSecondsRealtime(timeTillCrumble);
        crumble_enabled = false;
        gameObject.GetComponent<Renderer>().material = crumble_enabled ? defaultMaterial : crumbleMaterial;
        outerCollider.enabled = false;
        innerCollider.enabled = false;

        // Do the opposite once the other timer expires
        // NOTE: might need to do a while loop to ensure that it doesn't prematurely reactivate the material when not in the same dimention, although that might need to be
        // looked into once we get the apprpirate animations for everything
        yield return new WaitForSecondsRealtime(timeTillReturn);
        crumble_enabled = true;
        gameObject.GetComponent<Renderer>().material = crumble_enabled ? defaultMaterial : crumbleMaterial;
        outerCollider.enabled = true;
        innerCollider.enabled = true;
    }
}
