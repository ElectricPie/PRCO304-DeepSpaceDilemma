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
        if(Input.GetAxis(m_handGrip) > 0.5)
        {
            Debug.Log("Grabed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        m_lastCollision = other.gameObject;
    }

    private void OnTriggerEnteExit(Collider other)
    {
        if (other.gameObject == m_lastCollision)
        {
            m_lastCollision = null;
        }
    }
}
