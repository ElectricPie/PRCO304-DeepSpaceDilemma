using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] m_triggerTargets;

    protected void ActivateTrigger()
    {
        for (int i = 0; i < m_triggerTargets.Length; i++)
        {
            //Checks if the target has a the required script and if so calls the activate
            if (m_triggerTargets[i].GetComponent<ITriggerTarget>() != null) {
                m_triggerTargets[i].GetComponent<ITriggerTarget>().Activate(); 
            }
            else
            {
                Debug.LogWarning(this + ": target \"" + i + "\" \"" + m_triggerTargets[i] + "\" is missing Trigger Target");
            }
        }
    }
}
