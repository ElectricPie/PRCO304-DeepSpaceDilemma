using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : GrabableObject
{
    #region Private Serialized Variables
    [Tooltip("The amount of ammo that a magazine can hold")]
    [SerializeField]
    private int m_ammoCount = 30;

    [Tooltip("The name of the magazine type so the weapon can only accecpt the right type")]
    [SerializeField]
    private string m_magazineTypeName;
    #endregion


    #region Private Variables
    private MagZone m_magZone;
    #endregion


    #region Public Methods
    public override bool Grab(GameObject parent)
    {
        //Prevents sockets grabbing the magazine whist it is in a weapon
        if (parent.GetComponent<Socket>() && m_magZone != null)
        {
            return false;
        }

        //Return false if the base grab fails
        if (!base.Grab(parent))
        {
            return false;
        }

        //If the magazine is loaded attempt to remove it from the mag zone
        if (m_magZone != null)
        {
            m_magZone.RemoveMagazine();
            m_magZone = null;
        }

        //Return true when succesful
        return true;
    }

    /// <summary>
    /// Disables the outline shader if it is on the magazine
    /// </summary>
    public void DisableShader()
    {
        //Prevents the shader being enabled when in a weapon
        if (m_shaderController != null)
        {
            m_shaderController.DisableOutline();
        }
        else
        {
            Debug.LogError("Shader Controller Missing from <a>" + this.gameObject.name + "<a> ", this.gameObject);
        }
    }

    /// <summary>
    /// Reduces the ammo stored in the magazine by 1
    /// </summary>
    public void UseAmmo()
    {
        m_ammoCount--;
    }
    #endregion


    #region Properties
    public MagZone MagZone
    {
        set { m_magZone = value; }
    }

    public int Ammo
    {
        get { return m_ammoCount; }
    }

    public string MagazineType
    {
        get { return m_magazineTypeName; }
    }
    #endregion
}
