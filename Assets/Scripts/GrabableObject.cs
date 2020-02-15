using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    //Private
    private GameObject m_parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grab(GameObject parent)
    {
        //Set the collider to a trigger so that other things can still interact
        this.GetComponent<Collider>().isTrigger = true;
        this.GetComponent<Rigidbody>().useGravity = false;
        //Attach the object to the hand
        this.transform.SetParent(parent.transform);
        m_parent = parent;
    }

    public void Drop() {
        Debug.Log("Droping");
        m_parent = null;
        //Detatch the object from the parent
        this.transform.parent = null;
    }
}
