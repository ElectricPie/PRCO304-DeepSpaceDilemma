using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : GrabableObject
{
    #region Private Variables
    [SerializeField]
    private int m_ammoCount = 30;
    private MagZone m_magZone;
    #endregion


    #region Public Methods
    public override bool Grab(GameObject parent)
    {
        //Prevents sockets grabbing the magazine whist it is in a weapon
        if (parent.GetComponent<Socket>() && m_magZone != null)
        {
            return false;
        }

        //Return false if the base grab fails
        if (!base.Grab(parent))
        {
            return false;
        }

        //If the magazine is loaded attempt to remove it from the mag zone
        if (m_magZone != null)
        {
            m_magZone.RemoveMagazine();
            m_magZone = null;
        }

        //Return true when succesful
        return true;
    }

    public void UseAmmo()
    {
        m_ammoCount--;
    }
    #endregion


    #region Properties
    public MagZone MagZone
    {
        set { m_magZone = value; }
    }

    public int Ammo
    {
        get { return m_ammoCount; }
    }
    #endregion
}
