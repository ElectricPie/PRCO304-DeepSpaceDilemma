using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    protected List<TriggerTarget> m_triggerTargets;

    protected void ActivateTrigger()
    {
        foreach (TriggerTarget target in m_triggerTargets)
        {
            target.Activate();
        }
    }
}
