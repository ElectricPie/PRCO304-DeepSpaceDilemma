using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryGrabPoint : GrabableObject
{
    #region Private Variables
    private GameObject m_hand = null;
    #endregion


    #region Public Methods
    public override bool Grab(GameObject parent)
    {
        m_parent = parent;
        m_hand = parent.transform.GetChild(0).gameObject;
        
        m_hand.transform.parent = this.gameObject.transform;
        m_hand.transform.position = this.transform.position;
        m_hand.transform.localRotation = Quaternion.Euler( new Vector3(3.0f, 125.0f, 270.0f));
        return true;
    }

    public override void Drop()
    {
        //Resets the hand to the parents postion
        m_hand.transform.parent = m_parent.transform;
        m_hand.transform.position = m_parent.transform.position;
        m_hand.transform.rotation = m_parent.transform.rotation;

        //Removes the refences to the hand and parent
        m_parent = null;
        m_hand = null;
    }
    #endregion


    #region Gizmo Methods
    void OnDrawGizmos()
    {
        //Create a sphere where the grab point will be
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(this.transform.position, 0.05f);
    }
    #endregion


    #region Properties
    public bool IsGrabbed
    {
        get
        {
            if (m_hand == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    #endregion
}
