using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirggerTargetMove : MonoBehaviour, ITriggerTarget
{
    //Public
    public Vector3 targetDestination;

    //Private
    private Vector3 m_originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_originalPosition = this.transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        this.transform.position = targetDestination;
    }

    public void Deactivate()
    {
        this.transform.position = m_originalPosition;
    }

    void OnDrawGizmos()
    {
        //Create a sphere where the object will move to
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(targetDestination, 0.05f);
    }
}
