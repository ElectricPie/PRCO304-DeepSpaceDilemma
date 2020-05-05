using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Grab : MonoBehaviour
{
    #region Private Serialized Variables
    [Tooltip("Sets if the controller acts a the left hand")]
    [SerializeField]
    private bool m_isLeftHand = false;
    [Tooltip("The game object with the ammo pouch script")]
    [SerializeField]
    private AmmoPouch m_ammoPouch = null;
    #endregion


    #region Private Variables 
    private string m_handGrip = "";
    private GameObject m_lastCollision = null;
    private GameObject m_grabbedObject = null;
    #endregion


    #region MonoBehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {
        if (m_isLeftHand)
        {
            m_handGrip = "Left";
        }
        else
        {
            m_handGrip = "Right";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the grab input
        if (Input.GetButtonDown("VRGrab" + m_handGrip))
        {
            //Check if an object is already be held
            if (m_grabbedObject == null)
            {
                if (m_lastCollision != null)
                {
                    GameObject magazine = null;

                    //Attempts to access a ammo pouch and if that fails attempt to grab the object
                    if (m_lastCollision.GetComponent<AmmoPouch>())
                    {
                        m_lastCollision.GetComponent<AmmoPouch>().Grab(this.gameObject, out magazine);
                        magazine.GetComponent<GrabableObject>().Grab(this.gameObject);
                        m_grabbedObject = magazine;
                        m_lastCollision = null;
                    }
                    else if (m_lastCollision.GetComponent<GrabableObject>().Grab(this.gameObject))
                    {
                        m_grabbedObject = m_lastCollision;
                        m_lastCollision = null;

                        if (m_grabbedObject.GetComponent<Weapon>())
                        {
                            m_ammoPouch.MagazineType = m_grabbedObject.GetComponent<Weapon>().MagazineType;
                        }
                    }
                }
            }
            else
            {
                DropObject();
            }
        }

        //Pulling the trigger
        if (Input.GetButtonDown("VRTrigger" + m_handGrip))
        {
            //Check if holding and object
            if (m_grabbedObject != null)
            {
                m_grabbedObject.GetComponent<GrabableObject>().Interact();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks of the object has the grabable script
        if (other.GetComponent<GrabableObject>())
        {
            if (m_lastCollision != null && m_lastCollision.GetComponent<GrabableObject>().Parent == null)
            {
                m_lastCollision.GetComponent<ShaderController>().EnableOutline(1.05f, Color.yellow);
            }

            m_lastCollision = other.gameObject;

            if (m_lastCollision.GetComponent<GrabableObject>().Parent == null && m_lastCollision.GetComponent<ShaderController>())
            {
                m_lastCollision.GetComponent<ShaderController>().EnableOutline(1.05f, Color.green);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Checks that the object was the last collision and removes it if it is
        if (other.GetComponent<GrabableObject>() && other.gameObject == m_lastCollision)
        {
            if (m_lastCollision.GetComponent<GrabableObject>().Parent == null && m_lastCollision.GetComponent<ShaderController>())
            {
                m_lastCollision.GetComponent<ShaderController>().EnableOutline(1.05f, Color.yellow);
            }

            m_lastCollision = null;
        }
    }
    #endregion


    #region Public Methods
    /// <summary>
    /// Forces the character to drop a object
    /// </summary>
    public void DropObject()
    {
        if (m_grabbedObject != null)
        {
            m_grabbedObject.GetComponent<GrabableObject>().Drop();

            //Resets the ammo pouch
            if (m_grabbedObject.GetComponent<Weapon>())
            {
                m_ammoPouch.MagazineType = null;
            }

            m_grabbedObject = null;
        }
    }
    #endregion


    #region Properties
    public string Hand
    {
        get { return m_handGrip; }
    }
    #endregion
}
