using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTests : MonoBehaviour
{
    #region Monobehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Weapon>().fireMode = Weapon.FireMode.semi;
        InvokeRepeating("Fire", 1.0f, 0.3f);
    }
    #endregion
    
    
    #region Private Methods
    private void Fire()
    {
        //Fire the weapon
        this.GetComponent<Weapon>().Interact();
    }
    #endregion
}
