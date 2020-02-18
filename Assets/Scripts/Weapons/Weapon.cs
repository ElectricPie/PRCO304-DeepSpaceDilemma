using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GrabableObject
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

    //Private
    private float m_fireRate = 0.2f;
    private Magazine m_magazine;


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
            InvokeRepeating("ShootFullAuto", 0.0f, m_fireRate);
        }
        else if (fireMode == FireMode.safe)
        {
            Debug.Log("Weapon is on 'SAFE'");
        }
    }

    public void Reload(GameObject magazine)
    {
        m_magazine = magazine.GetComponent<Magazine>();

        //Checks if the magazine is being held
        if (magazine.GetComponent<Magazine>().Parent != null)
        {
            //Forces the magazine to be droped so it can be used
            magazine.GetComponent<Magazine>().Parent.GetComponent<Grab>().DropObject();
        }
    }

    public void RemoveMagazine()
    {
        m_magazine = null;
    }

    private void Shoot()
    {
        RaycastHit hit;
        //Create a raycast from the gun going forward for infinity
        if (Physics.Raycast(this.transform.position + new Vector3(0.0f,0.0f,0.4f), this.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
           
            //Check if there is a loaded magazine and that it has ammo
            if (m_magazine != null)
            {
                Debug.Log("Ammo Left: " + m_magazine.Ammo);
                if (m_magazine.Ammo > 0)
                {
                    //Use the ammo from the weapon
                    m_magazine.UseAmmo();
                    //TODO: Impliment damaging hit target

                    //TODO: Replace with creating decal
                    GameObject tempImpact = Instantiate(inpactDecal);
                    tempImpact.transform.position = hit.point;
                }
                else
                {
                    Debug.Log("Magazine Empty");
                }
            }
            else
            {
                Debug.Log("No Magazine Loaded");
            }
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
