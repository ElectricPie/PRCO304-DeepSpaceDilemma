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

    #region Public Variables
    [Tooltip("SAFE: Prevents firing | SEMI: Fires one bullet when trigger pulled | FULL: Fires a bullet depending on the fire rate")]
    public FireMode fireMode = FireMode.full;
    #endregion


    #region Private Serialized Variables
    [Tooltip("The point on the weapon that is place into the players hand")]
    [SerializeField]
    private Vector3 m_grabPoint = Vector3.zero;

    [Tooltip("The angle the weapon is angled at when it is grabed by a player")]
    [SerializeField]
    private float m_grabRotation = 36.0f;

    [Tooltip("The image that will apear at a bullet impact")]
    [SerializeField]
    //TODO: Replace with sprite
    private GameObject m_inpactDecal = null;

    [Tooltip("The amount of damage the weapon will deal if a shot hits a character")]
    [SerializeField]
    private int m_damage = 4;

    [Tooltip("The interval in seconds between bullets firing")]
    [SerializeField]
    private float m_fireRate = 0.2f;

    [Tooltip("The prefab magazine that is used for this weapon")]
    [SerializeField]
    private GameObject m_magazinePrefab = null;

    [Tooltip("The point where the projectiles will exit the weapon from")]
    [SerializeField]
    private GameObject m_firingPoint = null;

    [Tooltip("The amount the roation will increase by when the weapon is fired per second")]
    [SerializeField]
    private float m_recoilIncreaseAmount = 0.2f;

    [Tooltip("The maximum rotation the weapon will rotated whilst firing")]
    [Range(1,36)]
    [SerializeField]
    private float m_maxRecoil = 1.0f;

    [Tooltip("The game object with the secondary grab point attached to it")]
    [SerializeField]
    private SecondaryGrabPoint m_secondaryGrabPoint;
    #endregion


    #region Private Variables
    private bool m_isFiring = false;
    #endregion


    #region Monobehaviour Callbacks
    void Update()
    {
        float currrentXRotation = this.transform.localRotation.eulerAngles.x;
        
        //Rotates the weapon whilst it is firing
        if (m_isFiring && currrentXRotation > m_maxRecoil)
        {
            float recoilAmmount = m_recoilIncreaseAmount;
            //Reduces the recoil if a second point is grabed
            if (m_secondaryGrabPoint != null)
            {
                if (m_secondaryGrabPoint.IsGrabbed)
                {
                    recoilAmmount /= 2;
                }
            }
            else
            {
                Debug.LogWarning("Weapon <a>" + this + "</a> is missing secondary grab point");
            }

            this.transform.Rotate(Time.deltaTime * -recoilAmmount, 0, 0);
        }
        else 
        {
            //Stops the weapon rotating whilst not held and rotates it back to the grab rotation when not firing
            if (m_parent != null && currrentXRotation < m_grabRotation)
            {
                this.transform.Rotate(Time.deltaTime * m_recoilIncreaseAmount, 0, 0);
            }
        }
    }
    #endregion


    #region Private Variables

    private Magazine m_magazine = null;
    #endregion


    #region Public Methods
    public override bool Grab(GameObject parent)
    {
        //Return false if the base grab fails
        if (!base.Grab(parent))
        {
            return false;
        }

        Vector3 rotation = new Vector3(m_grabRotation, 0, 0);

        this.transform.localRotation = Quaternion.Euler(rotation);
        this.transform.localPosition = Vector3.zero - m_grabPoint;

        //Return true when succesful
        return true;
    }


    public override void Interact()
    {
        if (fireMode == FireMode.semi)
        {
            Shoot();
        }
        else if (fireMode == FireMode.full)
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
    #endregion


    #region Private Methods
    private void Shoot()
    {
        RaycastHit hit;
        //Create a raycast from the guns raycast start point going forward for infinity
        if (Physics.Raycast(m_firingPoint.transform.position, this.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {

            //Check if there is a loaded magazine
            if (m_magazine != null)
            {
                //Check that the magazine has ammo in it
                if (m_magazine.Ammo > 0)
                {
                    m_isFiring = true;

                    //Use the ammo from the weapon
                    m_magazine.UseAmmo();

                    //Deal damage to the first hit if they are a character
                    if (hit.transform.GetComponent<Character>())
                    {
                        hit.transform.GetComponent<Character>().TakeDamage(m_damage);
                        //TODO: Add effect for hitting character
                    }
                    else
                    {
                        //TODO: Replace with creating decal
                        GameObject tempImpact = Instantiate(m_inpactDecal);
                        tempImpact.transform.position = hit.point;
                    }
                }
                else
                {
                    m_isFiring = false;
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
            m_isFiring = false;
            return;
        }

        //Shoots if the trigger is still down
        Shoot();
    }
    #endregion


    #region Properties
    public GameObject MagazineType
    {
        get { return m_magazinePrefab; }
    }
    #endregion


    #region Gizmo Methods
    void OnDrawGizmos()
    {
        //Create a sphere where the grab point will be
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(this.transform.position + m_grabPoint, 0.05f);
        //Creates a green cube where the raycast for firing will be
        Gizmos.color = Color.green;
        Gizmos.DrawCube(m_firingPoint.transform.position, new Vector3(0.05f, 0.05f, 0.05f));
    }
    #endregion
}
