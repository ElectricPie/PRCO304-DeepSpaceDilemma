using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;

public class NonVRCharacterController : Character
{
    #region Public Variables
    public float speed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float gravity = 9.8f;

    public static GameObject localPlayerInstance;
    #endregion


    #region Private Variables
    private CharacterController m_characterController;
    private Camera m_camera;
    #endregion


    #region MonoBehavior Callbacks
    void Awake()
    {
        m_camera = this.transform.GetChild(0).GetComponent<Camera>();

        //Used to keep track of the clients player character
        if (photonView.IsMine) {
            localPlayerInstance = this.gameObject;
        }
        else
        {
            //Removes the camerea from the character if the client is connected 
            // and it is not the clients character
            if (PhotonNetwork.IsConnected)
            {
                Destroy(m_camera.gameObject);
            }
        }
        
        //Prevents the instance from being destroy so that level synchronization is smooth
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_characterController = this.GetComponent<CharacterController>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        //Stops the rest of the method if the character is not the clients
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        UpdateMovement();
        UpdateRotation();
        UpdateCamera();
    }
    

    void CalledOnLevelWasLoaded(int level)
    {
        //Checks if the character is a above an object, if it isnt then move the character
        if (!Physics.Raycast(transform.position, -Vector3.up, 5.0f))
        {
            transform.position = new Vector3(0.0f, 5.0f, 0.0f);
        }

        //Removes the camera if the character is not the clients
        if (!photonView.IsMine)
        {
            Destroy(m_camera);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion


    #region Private Methods
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

    private void UpdateRotation()
    {
        //Character rotation
        // Get rotation
        float rotation = Input.GetAxis("Mouse X");

        // Create rotation vector and apply rotation speed
        Vector3 rotationDirection = new Vector3(0, rotation, 0);
        rotationDirection *= rotationSpeed;

        //Get current character rotation and apply new rotation to it
        Vector3 newRotation = this.transform.rotation.eulerAngles;
        newRotation += rotationDirection;

        //Apply new rotation to chracter
        this.transform.rotation = Quaternion.Euler(newRotation);

    }

    private void UpdateCamera()
    {
        //Camera rotation
        Vector3 currentRotation = m_camera.transform.localRotation.eulerAngles;

        //Get the mouses y value
        float inputValue = Input.GetAxis("Mouse Y");
        
        //Check that the new rotation stays in the roation limit
        if (currentRotation.x < 45 && currentRotation.x >= 0)
        {
            if (currentRotation.x - inputValue < 45)
            {
                currentRotation.x -= inputValue;
            }
        }
        else if (currentRotation.x > 315 && currentRotation.x <= 360) { 
            if (currentRotation.x - inputValue > 315)
            {
                currentRotation.x -= inputValue;
            }
        }
        
        //Apply rotation to camera
        m_camera.transform.localRotation = Quaternion.Euler(currentRotation );
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }
    #endregion
}