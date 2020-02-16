using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    //Private
    private GameObject m_parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Attempts to grab the object, if the object is not currently grabbed
    /// returns true
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public bool Grab(GameObject parent)
    {
        //Returns false if the object is already attached
        if (m_parent != null)
        {
            return false;
        }

        //Set the collider to a trigger so that other things can still interact
        this.GetComponent<Collider>().isTrigger = true;
        this.GetComponent<Rigidbody>().useGravity = false;
        //Stops the object from moving if grabbed while moving
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.GetComponent<Rigidbody>().freezeRotation = true;
        //Attach the object to the hand
        this.transform.SetParent(parent.transform);
        m_parent = parent;

        return true;
    }

    public void Drop() {
        m_parent = null;
        //Detatch the object from the parent
        this.transform.parent = null;
        //Reenable the collider and gravity so that the object interacts with the world
        this.GetComponent<Collider>().isTrigger = false;
        this.GetComponent<Rigidbody>().useGravity = true;
        //Reneables movement
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.GetComponent<Rigidbody>().freezeRotation = false;
    }
}
