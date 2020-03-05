using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    #region MonoBehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        GrabableObject obj = other.GetComponent<GrabableObject>();

        Debug.Log("Other: " + obj);

        //Checks if the object is already being held
        if (obj != null && obj.Parent == null)
        {
            Debug.Log("Grabbing: <a>object</a>", obj.gameObject);
            obj.Grab(this.gameObject);
        }
        else
        {
            Debug.Log("Not Grabbable");
        }
    }
    #endregion
}
