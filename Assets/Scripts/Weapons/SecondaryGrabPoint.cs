using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryGrabPoint : GrabableObject
{
    #region Private Variables
 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Public Methods
    public override bool Grab(GameObject parent)
    {
        GameObject handChild = parent.transform.GetChild(0).gameObject;
        //return base.Grab(parent);
        handChild.transform.parent = this.gameObject.transform;
        handChild.transform.position = this.transform.position;

        return true;
    }

    public override void Drop()
    {
        //base.Drop();
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
}
