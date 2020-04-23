using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPouch : GrabableObject
{
    #region Private Variables
    private GameObject m_magazineType = null;
    #endregion

    public bool Grab(GameObject parent, out GameObject magazine)
    {
        //If a magazine type is set, create that magazine and set it to the parents position
        if (m_magazineType != null) {
            magazine = Instantiate(m_magazineType);
            magazine.transform.position = parent.transform.position;
            return true;
        }

        magazine = null;
        return false;
    }

    #region Properties
    public GameObject MagazineType
    {
        set { m_magazineType = value; }
    }
    #endregion
}
