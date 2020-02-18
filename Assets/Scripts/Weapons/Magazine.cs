using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : GrabableObject
{
    //Private
    [SerializeField]
    private int m_ammoCount = 30;
    private MagZone m_magZone;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool Grab(GameObject parent)
    {
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

    public MagZone MagZone
    {
        set { m_magZone = value; }
    }

    public int Ammo
    {
        get { return m_ammoCount; }
    }
}
