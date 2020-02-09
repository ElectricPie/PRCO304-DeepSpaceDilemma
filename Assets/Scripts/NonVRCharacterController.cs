using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRCharacterController : MonoBehaviour
{
    //Public
    public float speed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float gravity = 9.8f;

    //Private
    private CharacterController m_characterController;
    private Camera m_camera;

    // Start is called before the first frame update
    void Start()
    {
        m_characterController = this.GetComponent<CharacterController>();

        m_camera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();

        UpdateCamera();
    }

    private void UpdateMovement()
    {
        //Get movement input
        Vector3 moveDirection = Vector3.zero;
        // Gets the forward vector so that forward/backward movement will happen
        moveDirection += this.transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical");
        //Gets the right vector so that right/left movement will happen
        moveDirection += this.transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal");
        moveDirection *= speed;

        //Add gravity
        moveDirection.y -= gravity * Time.deltaTime;

        //Apply movement to the character
        m_characterController.Move(moveDirection * Time.deltaTime);
    }

    private void UpdateCamera()
    {
        float rotation = Input.GetAxis("Mouse X");

        Vector3 rotationDirection = new Vector3(0, rotation, 0);
        rotationDirection *= rotationSpeed;

        Vector3 newRotation = this.transform.rotation.eulerAngles;
        newRotation += rotationDirection;

        this.transform.rotation = Quaternion.Euler(newRotation);
    }
}