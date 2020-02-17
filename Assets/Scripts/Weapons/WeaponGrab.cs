using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGrab : GrabableObject
{
    public enum FireMode
    {
        safe,
        semi,
        full
    }

    //Public
    public Vector3 grabPoint;
    public float grabRotation = 36.0f;

    public int ammo = 30;
    //TODO: Replace with sprite
    public GameObject inpactDecal;

    public FireMode fireMode;

    private float fireRate = 0.2f;


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
        if (fireMode == FireMode.semi)
        {
            Shoot();
        }
        else if(fireMode == FireMode.full)
        {
            InvokeRepeating("ShootFullAuto", 0.0f, fireRate);
        }
        else if (fireMode == FireMode.safe)
        {
            Debug.Log("Weapon is on 'SAFE'");
        }
    }

    public void Reload(GameObject magazine)
    {
        //Checks if the magazine is being held
        if (magazine.GetComponent<GrabableObject>().Parent != null)
        {
            //Forces the magazine to be droped so it can be used
            magazine.GetComponent<GrabableObject>().Parent.GetComponent<Grab>().DropObject();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        //Create a raycast from the gun going forward for infinity
        if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //TODO: Impliment damaging hit target

            //TODO: Replace with creating decal
            GameObject tempImpact = Instantiate(inpactDecal);
            tempImpact.transform.position = hit.point;
        }
    }

    private void ShootFullAuto()
    {
        //Checks for release of the trigger
        if (!Input.GetButton("VRTrigger" + m_parent.GetComponent<Grab>().Hand))
        {
            //Stops the invoke and the function
            CancelInvoke("ShootFullAuto");
            return;
        }

        //Shoots if the trigger is still down
        Shoot();
    }
}
