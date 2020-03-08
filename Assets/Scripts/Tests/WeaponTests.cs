using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTests : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 1.0f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Private Methods
    private void Fire()
    {
        //Fire the weapon
        this.GetComponent<Weapon>().Interact();
    }
    #endregion
}
