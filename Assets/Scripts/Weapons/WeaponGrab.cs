using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGrab : GrabableObject
{
    public Vector3 grabPoint;
    public float grabRotation = 36.0f;

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

        Vector3 rotation = new Vector3(grabRotation, 0, 0);

        this.transform.localRotation = Quaternion.Euler(rotation);
        this.transform.localPosition = Vector3.zero - grabPoint;

        //Return true when succesful
        return true;
    }

    void OnDrawGizmos()
    {
        //Create a sphere where the grab point will be
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(this.transform.position + grabPoint, 0.05f);
    }

    public override void Interact()
    {
        Debug.Log("Firering Weapon");
    }
}
