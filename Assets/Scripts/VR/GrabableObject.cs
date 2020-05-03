using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ShaderController))]
public class GrabableObject : MonoBehaviour
{
    #region Protected Variables
    protected GameObject m_parent;
    #endregion


    #region Public Methods
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
            //Checks if the parent is a object socket otherwise the object shouldnt be grabbable.
            if (!m_parent.GetComponent<Socket>()) {
                return false;
            }
            else
            {
                //If the object is a socket it removes the object from the socket
                m_parent.GetComponent<Socket>().RemoveObject();
            }
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
    #endregion


    #region Properties 
    public GameObject Parent
    {
        get { return m_parent; }
    }
    #endregion
}
