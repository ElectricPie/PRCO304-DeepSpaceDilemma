using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : Trigger
{
    #region Private Seralizable Variables
    [SerializeField]
    private GameObject[] m_triggerItems;
    #endregion


    #region Monobehaviour Callbacks
    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < m_triggerItems.Length; i++) { 
            if (other.gameObject == m_triggerItems[i])
            {
                Debug.Log("ITrigger Item");
                base.ActivateTrigger();
            }
        }
    }
    #endregion


    #region Gizmo Callbacks
    void OnDrawGizmos()
    {
        Collider collider = this.GetComponent<Collider>();

        if (collider != null)
        {
            //Create a transparent green cube the size of the collider
            Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
            Gizmos.DrawCube(collider.bounds.center, collider.bounds.extents * 2);
        }
        else
        {
            Debug.LogWarning(this + " is missing a collider");
        }
    }
    #endregion
}
