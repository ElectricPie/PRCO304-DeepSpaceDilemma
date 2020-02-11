using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRCharacterController : MonoBehaviour
{
    //Public
    public float speed = 5.0f;
    public float gravity = 9.8f;

    //Private
    private CharacterController m_characterController;


    // Start is called before the first frame update
    void Start()
    {
        m_characterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Debug.Log("Left: " + Input.GetAxis("VRSecondaryAxisLeftX") + " | " + Input.GetAxis("VRSecondaryAxisLeftY"));

        //Get movement input
        Vector3 moveDirection = Vector3.zero;
        // Gets the forward vector so that forward/backward movement will happen
        moveDirection += this.transform.TransformDirection(Vector3.forward) * Input.GetAxis("VRSecondaryAxisLeftY");
        //Gets the right vector so that right/left movement will happen
        moveDirection += this.transform.TransformDirection(Vector3.right) * Input.GetAxis("VRSecondaryAxisLeftX");
        moveDirection *= speed;

        //Add gravity
        moveDirection.y -= gravity * Time.deltaTime;

        //Apply movement to the character
        m_characterController.Move(moveDirection * Time.deltaTime);
    }
}


