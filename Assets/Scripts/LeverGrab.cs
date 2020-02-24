using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverGrab : GrabableObject
{
    [Range(-60, -1)]
    public float activateValue = -45.0f;
    [Range(1, 60)]
    public float deactivateValue = 45.0f;

    // Start is called before the first frame update
    void Start()
    {

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
        
        if (xRotation < activateValue)
        {
            Activate();
        }
        else if (xRotation > deactivateValue)
        {
            Deactivate();
        }
    }

    private void Activate()
    {
        Debug.Log("Activated");
    }

    private void Deactivate()
    {
        Debug.Log("Deactivate");
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