using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverGrab : GrabableObject
{
    public float adjustmentAmount = 270.0f;

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

        Debug.Log("Relesed Lever");
    }
}