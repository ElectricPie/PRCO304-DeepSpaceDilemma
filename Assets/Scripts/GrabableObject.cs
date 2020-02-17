using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabableObject : MonoBehaviour
{
    //Protected
    protected GameObject m_parent;

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
    public virtual bool Grab(GameObject parent)
    {
        //Returns false if the object is already attached
        if (m_parent != null)
        {
            return false;
        }

        
        //Set the collider to a trigger so that other things can still interact
        this.GetComponent<Collider>().isTrigger = true;
        //Disables the rigidbody
        this.GetComponent<Rigidbody>().isKinematic = true;
       
        //Attach the object to the hand
        this.transform.SetParent(parent.transform);
        m_parent = parent;

        return true;
    }

    public virtual void Drop() {
        m_parent = null;
        //Detatch the object from the parent
        this.transform.parent = null;
        //Reenable the collider and gravity so that the object interacts with the world
        this.GetComponent<Collider>().isTrigger = false;
        //Enables the rigidbody
        this.GetComponent<Rigidbody>().isKinematic = false;

    }

    public virtual void Interact()
    {
        
    }
}
