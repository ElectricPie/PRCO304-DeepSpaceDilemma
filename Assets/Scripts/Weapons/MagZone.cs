using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagZone : MonoBehaviour
{
    #region Private Variables
    private Weapon m_weapon;
    private GameObject m_currentMag;
    #endregion


    #region Monobehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {
        m_weapon = this.transform.parent.GetComponent<Weapon>();

        if (m_weapon == null)
        {
            Debug.LogError("PARENT WEAPON NOT FOUND");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks of the object has the magazine grab script and checks if there is a magazine already loaded
        if (other.GetComponent<Magazine>() && m_currentMag == null)
        {
            //Checks that the magazine is the correct type for the weapon
            if (other.GetComponent<Magazine>().MagazineType == m_weapon.MagazineType.GetComponent<Magazine>().MagazineType)
            {
                //Checks if the magazine has a parent
                if (other.GetComponent<Magazine>().Parent != null)
                {
                    //If the magazine has a parent that is a socket then prevent reloading
                    if (other.GetComponent<Magazine>().Parent.GetComponent<Socket>() != null)
                    {
                        return;
                    }
                }

                m_currentMag = other.gameObject;
                m_weapon.Reload(other.gameObject);
                m_currentMag.GetComponent<Magazine>().MagZone = this;
                //Attach the magazine to the weapon
                m_currentMag.transform.parent = m_weapon.gameObject.transform;
                //Correctly sets the magazines position and rotation
                m_currentMag.transform.localPosition = this.transform.localPosition;
                m_currentMag.transform.localRotation = Quaternion.Euler(Vector3.zero);
                //Prevents the new magazine from moving
                m_currentMag.GetComponent<Rigidbody>().isKinematic = true;
                m_currentMag.GetComponent<Collider>().isTrigger = true;
            }
        }
    }
    #endregion


    #region Public Methods
    public void RemoveMagazine()
    {
        m_currentMag = null;
        m_weapon.RemoveMagazine();
    }
    #endregion
}
