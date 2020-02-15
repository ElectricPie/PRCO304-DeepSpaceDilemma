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
        if (Input.GetButtonDown(m_handGrip))
        {
            //Check if an object is already be held
            if (m_grabbedObject == null)
            {
                if (m_lastCollision != null)
                {
                    //Attempt to grab the object
                    if (m_lastCollision.GetComponent<GrabableObject>().Grab(this.gameObject)) {
                        m_grabbedObject = m_lastCollision;
                        m_lastCollision = null;
                    }
                }
            }
            else
            {
                m_grabbedObject.GetComponent<GrabableObject>().Drop();
                //m_lastCollision = m_grabbedObject;
                m_grabbedObject = null;
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
