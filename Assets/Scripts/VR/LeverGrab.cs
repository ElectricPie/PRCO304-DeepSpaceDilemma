using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LeverTrigger))]
public class LeverGrab : GrabableObject
{
    //Public
    [Range(-60, -1)]
    public float activateValue = -45.0f;
    [Range(1, 60)]
    public float deactivateValue = 45.0f;

    //Private
    private LeverTrigger m_trigger;
    private bool m_isActive;

    // Start is called before the first frame update
    void Start()
    {
        m_trigger = this.GetComponent<LeverTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_parent != null)
        {
            //Rotates the lever to face the target
            this.transform.LookAt(m_parent.transform.position);
            //Adjust the rotation so it correctly faces the players hand and prevents roation on the y and z axis
            Vector3 rotation = -this.transform.rotation.eulerAngles;
            rotation.y = 0;
            rotation.z = 0;

            this.transform.rotation = Quaternion.Euler(rotation);
        }

        //Gets the current x roatation and makes the value between -90 and 90
        float xRotation = this.transform.localRotation.eulerAngles.x;
        if (xRotation > 90)
        {
            xRotation -= 360;
        }
        
        if (xRotation < activateValue && !m_isActive)
        {
            Activate();
        }
        else if (xRotation > deactivateValue && m_isActive)
        {
            Deactivate();
        }
    }

    private void Activate()
    {
        Debug.Log("Activated");
        if (m_trigger != null)
        {
            m_trigger.Activate();
        }

        m_isActive = true;
    }

    private void Deactivate()
    {
        Debug.Log("Deactivate");
        if (m_trigger != null)
        {
            m_trigger.Deactivate();
        }

        m_isActive = false;
    }

    public override bool Grab(GameObject parent)
    {
        //Returns false if the object is already attached
        if (m_parent != null)
        {
            return false;
        }

        m_parent = parent;

        return true;
    }

    public override void Drop()
    {
        m_parent = null;
    }
}