using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirggerTargetMove : MonoBehaviour, ITriggerTarget
{
    #region Private Seralizable Variables
    [SerializeField]
    private Vector3 m_targetDestination;
    #endregion


    #region Private Variables
    private Vector3 m_originalPosition;
    private bool m_isOpen = false;
    #endregion


    #region Monobehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {
        m_originalPosition = this.transform.localPosition;   
    }
    #endregion


    #region Public Methods
    public void Activate()
    {
        if (m_isOpen)
        {
            this.transform.localPosition = m_originalPosition;
        }
        else
        {
            this.transform.localPosition = m_originalPosition + m_targetDestination;
        }
        m_isOpen = !m_isOpen;
    }

    public void Deactivate()
    {
        Activate();
    }
    #endregion


    #region Gizmo Methods
    void OnDrawGizmos()
    {
        //Create a sphere where the object will move to
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere((this.transform.localPosition + m_targetDestination), 0.1f);
    }
    #endregion
}
