using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRCharacterController : MonoBehaviour
{
    //Private
    private Camera m_camera;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = this.GetComponent<Camera>();
    
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
