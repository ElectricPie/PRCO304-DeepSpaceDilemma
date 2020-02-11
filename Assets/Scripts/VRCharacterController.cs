using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Left: " + Input.GetAxis("VRSecondaryAxisLeftX") + " | " + Input.GetAxis("VRSecondaryAxisLeftY"));
        Debug.Log("Right: " + Input.GetAxis("VRSecondaryAxisRightX") + " | " + Input.GetAxis("VRSecondaryAxisRightY"));
    }
}


