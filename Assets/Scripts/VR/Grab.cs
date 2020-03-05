﻿using System.Collections;
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
            m_handGrip = "Left";
        }
        else
        {
            m_handGrip = "Right";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the grab input
        if (Input.GetButtonDown("VRGrab" + m_handGrip))
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
                DropObject();
            }
        }

        //Pulling the trigger
        if (Input.GetButtonDown("VRTrigger" + m_handGrip))
        {
            //Check if holding and object
            if (m_grabbedObject != null)
            {
                m_grabbedObject.GetComponent<GrabableObject>().Interact();
            }
        }
    }

    /// <summary>
    /// Forces the character to drop a object
    /// </summary>
    public void DropObject()
    {
        if (m_grabbedObject != null)
        {
            m_grabbedObject.GetComponent<GrabableObject>().Drop();
            m_grabbedObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks of the object has the grabable script
        if (other.GetComponent<GrabableObject>())
        {
            m_lastCollision = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Checks that the object was the last collision and removes it if it is
        if (other.GetComponent<GrabableObject>() && other.gameObject == m_lastCollision)
        {
            m_lastCollision = null;
        }
    }

    public string Hand
    {
        get { return m_handGrip; }
    }
}