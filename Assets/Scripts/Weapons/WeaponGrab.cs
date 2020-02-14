using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WeaponGrab : MonoBehaviour
{
    public Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        //Check if the public collider was set
        if (collider == null)
        {
            //Get the collider on the object
            this.GetComponent<Collider>();

            //Checks if the gathered collider was added.
            if (collider == null)
            {
                Debug.LogError("No collider found on: " + this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }   
    
    public void GrabWeapon()
    {
        Debug.Log("Grabed Weapon");
        collider.enabled = false;
    }

    public void DropWeapon()
    {
        Debug.Log("Dropped Weapon");
        collider.enabled = true;
    }
}
