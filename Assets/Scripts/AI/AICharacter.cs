using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AICharacter : Character
{
    #region Private Vairables
    [Tooltip("The world location the AI will move to")]
    [SerializeField]
    private Vector3 m_destination;
    private NavMeshAgent m_agent;
    #endregion


    #region Monobehaviour Callbacks
    void Awake()
    {
        m_agent = this.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        MoveToDestination();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion


    #region Private Methods
    private void MoveToDestination()
    {
        m_agent.SetDestination(m_destination);
    }
    #endregion


    #region Gizmo Methods
    void OnDrawGizmos()
    {
        //Create a sphere where the grab point will be
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(m_destination, 0.1f);
    }
    #endregion
}
