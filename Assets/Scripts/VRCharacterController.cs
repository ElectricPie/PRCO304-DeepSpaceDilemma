using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRCharacterController : MonoBehaviour
{
    //Public
    public float speed = 5.0f;
    public float gravity = 9.8f;

    public Camera camera;

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
        //Get movement input
        Vector3 moveDirection = Vector3.zero;
        //Gets the forward vector from the camera so that forward/backward movement will happen
        moveDirection += camera.transform.TransformDirection(Vector3.forward) * Input.GetAxis("VRSecondaryAxisLeftY");
        //Gets the right vector from the camera so that right/left movement will happen
        moveDirection += camera.transform.TransformDirection(Vector3.right) * Input.GetAxis("VRSecondaryAxisLeftX");
        //Removes the Y position added from the camera
        moveDirection.y = 0;
        moveDirection *= speed;

        //Add gravity
        moveDirection.y -= gravity * Time.deltaTime;

        //Apply movement to the character
        m_characterController.Move(moveDirection * Time.deltaTime);
    }
}


