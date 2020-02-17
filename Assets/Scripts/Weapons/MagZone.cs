using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagZone : MonoBehaviour
{
    //Public
    public GameObject currentMag;

    //Private
    private WeaponGrab m_weapon;

    // Start is called before the first frame update
    void Start()
    {
        m_weapon = this.transform.parent.GetComponent<WeaponGrab>();

        if (m_weapon == null)
        {
            Debug.LogError("PARENT WEAPON NOT FOUND");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks of the object has the magazine grab script
        if (other.GetComponent<MagazineGrab>())
        {
            m_weapon.Reload(other.gameObject);
        }
    }
}
