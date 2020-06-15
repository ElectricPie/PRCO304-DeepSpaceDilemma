using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;

using TMPro;
using System;

public class VRCharacterController : Character
{
    #region Public Variables
    public float speed = 5.0f;
    public float gravity = 9.8f;

    public GameObject body = null;
    public static GameObject localPlayerInstance = null;
    #endregion


    #region Private Serialize Variables
    [Tooltip("The object with the PlayerHealthBar script")]
    [SerializeField]
    private PlayerHealthBar m_healthBar = null;

    [Tooltip("The Players text display text mesh object")]
    [SerializeField]
    private TextMeshPro m_playerTextDisplay = null;
    #endregion


    #region Private Variables
    private CharacterController m_characterController = null;
    private float m_quitTimer = 0;
    #endregion


    #region MonoBehavior Callbacks
    void Awake()
    {
        //Set up the chracter if the client is connected to the server
        if (PhotonNetwork.IsConnected)
        {
            //Used to keep track of the clients player character
            if (photonView.IsMine)
            {
                localPlayerInstance = this.gameObject;
            }
            else
            {
                Destroy(this.transform.GetChild(0).gameObject);
            }

            //Prevents the instance from being destroy so that level synchronization is smooth
            DontDestroyOnLoad(this.gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        m_characterController = this.GetComponent<CharacterController>();

        SceneManager.sceneLoaded += OnSceneLoaded;        
    }

    // Update is called once per frame
    void Update()
    {
        //Stop the chracter if the client is connected to the server and the character is not the client
        if (PhotonNetwork.IsConnected)
        {
            //Debug.Log("Connected: " + PhotonNetwork.IsConnected + " | IsClients: " + photonView.IsMine);
            //Stops the rest of the method if the character is not the clients
            if (PhotonNetwork.InRoom && !photonView.IsMine)
            {
                return;
            }
        }

        UpdateMovement();


        if (Input.GetButton("Menu"))
        {
            //Activate the text display
            if (m_playerTextDisplay !=null)
            {
                m_playerTextDisplay.gameObject.SetActive(true);
                m_playerTextDisplay.text = "QUITTING IN: " + Math.Floor(4 - m_quitTimer);
            }

            //Quits the program if the player holds down the button for 3 seconds or more
            if (m_quitTimer >= 4)
            {
                Application.Quit();
            }

            m_quitTimer += Time.deltaTime;
        }

        if (Input.GetButtonUp("Menu"))
        {
            //Resets the timer
            m_quitTimer = 0;

            //Dissables the text display
            if (m_playerTextDisplay != null)
            {
                m_playerTextDisplay.gameObject.SetActive(false);
            }
        }
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
            this.transform.GetChild(0).gameObject.SetActive(false);
        }   
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion


    #region Public Methods
    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);
        UpdatePlayerHealthDisplay();
    }
    #endregion


    #region Private Methods
    private void UpdateMovement()
    {
        //Debug.Log("Moving");
        //Get movement input
        Vector3 moveDirection = Vector3.zero;
        //Gets the forward vector from the camera so that forward/backward movement will happen
        moveDirection += body.transform.TransformDirection(Vector3.forward) * Input.GetAxis("VRSecondaryAxisLeftY");
        //Gets the right vector from the camera so that right/left movement will happen
        moveDirection += body.transform.TransformDirection(Vector3.right) * Input.GetAxis("VRSecondaryAxisLeftX");
        //Removes the Y position added from the camera
        moveDirection.y = 0;
        moveDirection *= speed;

        //Add gravity
        moveDirection.y -= gravity * Time.deltaTime;

        //Apply movement to the character
        m_characterController.Move(moveDirection * Time.deltaTime);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }

    private void UpdatePlayerHealthDisplay()
    {
        m_healthBar.UpdateHealth(m_currentHealth, m_startingHealth);
    }
    #endregion
}


