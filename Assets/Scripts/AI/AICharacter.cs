using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AICharacter : Character
{
    #region Private Serialize Vairables
    [Tooltip("The world location the AI will move to")]
    [SerializeField]
    private Vector3 m_destination;
    #endregion


    #region Private Vairables
    private NavMeshAgent m_agent;
    private List<GameObject> m_charactersInRange;
    private GameObject m_target;
    #endregion


    #region Monobehaviour Callbacks
    void Awake()
    {
        m_agent = this.GetComponent<NavMeshAgent>();
        m_charactersInRange = new List<GameObject>();
        m_target = null;
    }

    void Start()
    {
        base.Start();
        //MoveToDestination();
    }

    // Update is called once per frame
    void Update()
    {
        //Looks for a target if it one is within its trigger sphere
        if (m_target == null)
        {
            foreach (GameObject character in m_charactersInRange)
            {
                RaycastHit hit;
                //Create a raycast from the AI to the character to see if anything is in the way
                if (Physics.Raycast(this.transform.localPosition, character.transform.position - this.transform.position, out hit, Mathf.Infinity))
                {
                    if (hit.transform.GetComponent<Character>())
                    {
                        m_target = hit.transform.gameObject;
                        MoveToDestination();
                        Debug.Log("Ai has set <a>target</a>", m_target);
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            m_charactersInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        m_charactersInRange.Remove(other.gameObject);
        if (other.gameObject == m_target)
        {
            m_target = null;
        }
    }
    #endregion


    #region Private Methods
    private void MoveToDestination()
    {
        m_agent.SetDestination(m_target.transform.position);
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
