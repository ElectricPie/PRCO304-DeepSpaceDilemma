using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRCharacterController : MonoBehaviour
{
    //Public
    public float speed = 1.0f;
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
        //Get movement input
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection *= speed;

        //Add gravity
        moveDirection.y -= gravity * Time.deltaTime;

        //Apply movement to the character
        m_characterController.Move(moveDirection * Time.deltaTime);
    }
}