using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonTrigger : Trigger
{
    #region Private Serialized Variables
    [Tooltip("Sets the button so that only the host of the lobby can use it")]
    [SerializeField]
    private bool m_hostUseOnly;
    [Tooltip("Sets the button so that only the trigger only affects this client")]
    [SerializeField]
    private bool m_clientAffectOnly;
    [Tooltip("Sets if the button requires another press to deactivate the targets")]
    [SerializeField]
    private bool m_toggleButton = false;
    #endregion


    #region Private Variables
    private bool m_isActive = false;

    private List<GameObject> m_currentColliders;

    private Collider m_triggerArea;
    #endregion


    #region Monobehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {
        m_currentColliders = new List<GameObject>();
    }

    void Update()
    {
        //TODO: Remove when not needed
        if (m_activateTriggers)
        {
            UseButton();
            m_activateTriggers = !m_activateTriggers;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //TODO: Add check for master
        //Checks if the other collider is a hand
        if (other.gameObject.GetComponent<XRController>())
        {
            m_currentColliders.Add(other.gameObject);

            //TODO: Only change client settings
            UseButton();
        }
    }

    void OnTriggerExit(Collider other)
    {
        m_currentColliders.Remove(other.gameObject);

        //Checks if the button is a togglable and if it isn't and there is nothing holding it down
        if (!m_toggleButton && m_currentColliders.Count == 0)
        {
            DeactivateTrigger();
        }
    }
    #endregion


    #region Private Methods
    private void UseButton()
    {
        if (m_toggleButton)
        { 
            if (m_isActive)
            {
                DeactivateTrigger();
            }
            else
            {
                ActivateTrigger();
            }

            m_isActive = !m_isActive;
        }
        else
        {
            ActivateTrigger();
        }
    }
    #endregion
}
