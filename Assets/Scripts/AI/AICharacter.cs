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

    [Tooltip("The distance from the AI to the target before the AI can attack the target")]
    [SerializeField]
    private float m_engagementDistance = 5.0f;

    [Tooltip("The delay in seconds between the AIs attacks")]
    [SerializeField]
    private float m_attackTime = 3.0f;

    [Tooltip("The amount of damage that the AI will deal to its target")]
    [SerializeField]
    private int m_attackDamage = 2;
    #endregion


    #region Private Vairables
    private NavMeshAgent m_agent;
    private List<GameObject> m_charactersInRange;
    private GameObject m_target = null;
    private bool m_isAttacking = false;
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
        else
        {
            //Checks if the target is in range
            if (Vector3.Distance(this.transform.position, m_target.transform.position) <= m_engagementDistance && !m_isAttacking)
            {
                m_agent.isStopped = true;
                Debug.Log("<a>AI Character</a> has started attacking", this.gameObject);
                InvokeRepeating("AttackTarget", m_attackTime, m_attackTime);
                m_isAttacking = true;
            }
            else if (Vector3.Distance(this.transform.position, m_target.transform.position) > m_engagementDistance)
            {
                Debug.Log(" <a>AI Character</a> has stopped attacking", this.gameObject);
                CancelInvoke("AttackTarget");
                MoveToDestination();
                m_isAttacking = false;
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

    private void AttackTarget() 
    {
        Debug.Log("Ai has attacked <a>target</a>", m_target);
        if (m_target != null)
        {
            m_target.GetComponent<Character>().TakeDamage(m_attackDamage);
        }
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
