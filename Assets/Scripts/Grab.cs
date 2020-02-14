using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Grab : MonoBehaviour
{    
    //Public
    public bool isLeftHand;

    //Private 
    private string m_handGrip;
    private GameObject m_lastCollision;
    private GameObject m_grabbedObject;

    // Start is called before the first frame update
    void Start()
    {
        if (isLeftHand)
        {
            m_handGrip = "VRSecondaryAxisLeftGrab";
        }
        else
        {
            m_handGrip = "VRSecondaryAxisRightGrab";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the grab input
        if (Input.GetAxis(m_handGrip) > 0.5)
        {
            //Check if an object is already be held
            if (m_grabbedObject == null)
            {
                if (m_lastCollision != null)
                {
                    m_grabbedObject = m_lastCollision;
                    m_lastCollision = null;

                    //Set the collider to a trigger so that other things can still interact
                    m_grabbedObject.GetComponent<Collider>().isTrigger = true;
                    m_grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                    //Attach the object to the hand
                    m_grabbedObject.transform.SetParent(this.transform);
                }
            }
            else
            {
                //TODO: Drop item
            }
        }
        /*
        if (m_grabbedObject != null)
        {
            if (m_grabbedObject.transform.parent != this)
            {
                Debug.LogError("Not parent");
                m_grabbedObject = null;
            }
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Other Tag: " + other.tag);

        if (other.GetComponent<GrabableObject>())
        {
            //Debug.Log("other: " + other);
            m_lastCollision = other.gameObject;
        }
    }

    private void OnTriggerEnteExit(Collider other)
    {
        if (other.GetComponent<GrabableObject>() && other.gameObject == m_lastCollision)
        {
            m_lastCollision = null;
        }
    }
}
