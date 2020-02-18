using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineGrab : GrabableObject
{
    //Private
    private MagZone m_magZone;

    // Start is called before the first frame update
    void Start()
    {
        //Prevents the magazine and weapon from colliding
        //Physics.IgnoreCollision(this.transform.parent.GetComponent<Collider>(), this.GetComponent<Collider>());
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

    public MagZone MagZone
    {
        set { m_magZone = value; }
    }
}
