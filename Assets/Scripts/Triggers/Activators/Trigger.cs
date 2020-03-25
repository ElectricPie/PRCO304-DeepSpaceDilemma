using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    #region Protected Variables
    [Tooltip("The objects that will be changed when the trigger is used")]
    [SerializeField]
    protected GameObject[] m_triggerTargets;
    #endregion


    #region Private Variables
    //TODO: Remove when not needed
    [Tooltip("Forces the trigger to activate the targets")]
    [SerializeField]
    protected bool m_activateTriggers;
    #endregion


    #region Monobehavior Callbacks
    void Update()
    {
        //TODO: Remove when not needed
        if (m_activateTriggers) {
            ActivateTrigger();
            m_activateTriggers = !m_activateTriggers;
        }
    }
    #endregion


    #region Protected Methods
    protected void ActivateTrigger()
    {
        for (int i = 0; i < m_triggerTargets.Length; i++)
        {
            //Checks if the target has a the required script and if so calls the activate
            if (m_triggerTargets[i].GetComponent<ITriggerTarget>() != null) 
            {
                m_triggerTargets[i].GetComponent<ITriggerTarget>().Activate(); 
            }
            else
            {
                Debug.LogWarning(this + ": target \"" + i + "\" \"" + m_triggerTargets[i] + "\" is missing Trigger Target");
            }
        }
    }

    protected void DeactivateTrigger()
    {
        for (int i = 0; i < m_triggerTargets.Length; i++)
        {
            //Checks if the target has a the required script and if so calls the activate
            if (m_triggerTargets[i].GetComponent<ITriggerTarget>() != null)
            {
                m_triggerTargets[i].GetComponent<ITriggerTarget>().Deactivate();
            }
            else
            {
                Debug.LogWarning(this + ": target \"" + i + "\" \"" + m_triggerTargets[i] + "\" is missing Trigger Target");
            }
        }
    }
    #endregion
}
