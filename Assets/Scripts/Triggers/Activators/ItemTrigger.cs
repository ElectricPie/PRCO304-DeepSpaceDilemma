using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : Trigger
{
    public int test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TriggerItem>())
        {
            Debug.Log("ITrigger Item");
            base.ActivateTrigger();
        }
    }

    void OnDrawGizmos()
    {
        Collider collider = this.GetComponent<Collider>();

        if (collider != null)
        {
            //Create a transparent green cube the size of the collider
            Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
            Gizmos.DrawCube(collider.bounds.center, collider.bounds.extents * 2);
        }
        else
        {
            Debug.LogWarning(this + " is missing a collider");
        }
    }
}
