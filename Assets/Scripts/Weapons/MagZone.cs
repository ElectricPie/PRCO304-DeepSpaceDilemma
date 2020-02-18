﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagZone : MonoBehaviour
{
    //Private
    private WeaponGrab m_weapon;
    public GameObject m_currentMag;

    // Start is called before the first frame update
    void Start()
    {
        m_weapon = this.transform.parent.GetComponent<WeaponGrab>();

        if (m_weapon == null)
        {
            Debug.LogError("PARENT WEAPON NOT FOUND");
        }
    }

    public void RemoveMagazine()
    {
        m_currentMag = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks of the object has the magazine grab script and checks if there is a magazine already loaded
        if (other.GetComponent<MagazineGrab>() && m_currentMag == null)
        {
            m_currentMag = other.gameObject;
            m_weapon.Reload(other.gameObject);
            m_currentMag.GetComponent<MagazineGrab>().MagZone = this;
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
