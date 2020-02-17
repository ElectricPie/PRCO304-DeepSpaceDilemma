using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineGrab : GrabableObject
{
    // Start is called before the first frame update
    void Start()
    {
        //Prevents the magazine and weapon from colliding
        Physics.IgnoreCollision(this.transform.parent.GetComponent<Collider>(), this.GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
