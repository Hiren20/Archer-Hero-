using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    public GameObject arrowRenderer;

    bool isSticked = false;
    public GameObject arrowMeesh;

    
 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isSticked )
        {
            isSticked = true;
         //   arrowRenderer.transform.parent = collision.contacts[0].thisCollider.transform;
            arrowRenderer.transform.parent = collision.transform;
           
            Destroy(gameObject, 1f);

            if(collision.gameObject.layer !=  LayerMask.NameToLayer("Water"))
            {
                FindObjectOfType<PlayerController>().hitArrow += 1;
            }
        }
    }
}
