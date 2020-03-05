using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    #region Private Vairables
    private Collider m_collider;
    private GameObject m_storedObject;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        m_collider = this.GetComponent<Collider>();
    }

    void OnTriggerStay(Collider other)
    {
        GrabableObject obj = other.GetComponent<GrabableObject>();

        //Checks if the object is already being held and grabs it if not
        if (obj != null && obj.Parent == null)
        {
            if (obj.Grab(this.gameObject))
            {
                m_storedObject = obj.gameObject;
                //Disables the collider from grabbing other object
                m_collider.enabled = !m_collider.enabled;
            }
        }
        else
        {
            Debug.Log("Not Grabbable");
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Removes the stored object from the socket
    /// </summary>
    public void RemoveObject()
    {
        m_storedObject = null;
        //Reenenable the collider
        m_collider.enabled = !m_collider.enabled;
    }
    #endregion
}
