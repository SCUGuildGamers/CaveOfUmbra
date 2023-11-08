using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;


// Most of this is copied from SwitchingMechanic, which is like making a circle block go in a square hole.
// Fight me 

public class CrumblingPlatform : MonoBehaviour
{
    public bool enabled = true;
    // Once contact made, amount time taken before platform disapears
    public float timeTillPlatformCrumble = 1; 
    public float timeCount = 0;
    public float timeTillPlatformReturns = 3;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material transparentMaterial;
    public PlayerController pc = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        Debug.Log("Trigger active)");
        if (enabled)
            pc.groundLayer |= (1 << (int)Mathf.Log(gameObject.layer, 2));
        else
            pc.groundLayer &= ~(1 << (int)Mathf.Log(gameObject.layer, 2));
        if(otherCollider.CompareTag("Player"))
        {
            // Crumble();
            Debug.Log("Yay");
        }
    }

    private void Crumble()
    {
        BoxCollider2D ObjectCollider = gameObject.GetComponent<BoxCollider2D>();

        timeCount += Time.deltaTime;
        if(timeCount >= timeTillPlatformCrumble)
        {
            // Resets time count
            timeCount = 0;

            //Swaps material
            gameObject.GetComponent<Renderer>().material = enabled ? defaultMaterial : transparentMaterial;


            // From here
            /*
            Transform innerCollider = gameObject.transform.Find("InnerBoxCollider");
            if (innerCollider != null)
                innerCollider.gameObject.SetActive(enabled);

            //Stops moving platforms from moving player when not in same dimension
            PlatformBinding pb = gameObject.GetComponentInChildren<PlatformBinding>(true);
            if (pb != null)
                pb.gameObject.SetActive(enabled);
            // to here, don't know if this is necessary
            */


            // Deactivate trigger
            ObjectCollider.isTrigger = false;

            // Restart time count for return
            timeCount += Time.deltaTime;
            if(timeCount >= timeTillPlatformReturns)
            {
                // Resets time count
                timeCount = 0;

                //Swaps material
                gameObject.GetComponent<Renderer>().material = enabled ? defaultMaterial : transparentMaterial;


                // From here
                /*
                Transform innerCollider = gameObject.transform.Find("InnerBoxCollider");
                if (innerCollider != null)
                    innerCollider.gameObject.SetActive(enabled);

                //Stops moving platforms from moving player when not in same dimension
                PlatformBinding pb = gameObject.GetComponentInChildren<PlatformBinding>(true);
                if (pb != null)
                    pb.gameObject.SetActive(enabled);
                    */
                // to here, don't know if this is necessary


                // reactivate trigger
                ObjectCollider.isTrigger = true;
            }
        }
    }

}