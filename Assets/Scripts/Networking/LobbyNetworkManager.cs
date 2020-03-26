using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(MenuLobbyController))]
public class LobbyNetworkManager : MonoBehaviourPunCallbacks
{
    #region Private Variables
    [Tooltip("The maximum number of players a room can hold")]
    [SerializeField]
    private byte m_maxPlayersPerRoom = 5;
    #endregion


    #region Private Variables
    private string m_gameVersion = "1";
    private string m_roomCode = "";
    private bool m_isConnecting;
    #endregion


    #region Monobehavior Callbacks
    void Awake()
    {
        //Makes sure all clients in the room use the same scene
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion


    #region MonoBehaviourPunCallbacks Callbacks
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room with " + PhotonNetwork.CurrentRoom.PlayerCount + " players");
        Debug.Log("In Lobby: " + PhotonNetwork.CurrentRoom.Name);

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //Generates code utill an unqiue one is created
        while (!PhotonNetwork.InRoom)
        {
            m_roomCode = GenerateCode();

            PhotonNetwork.CreateRoom(m_roomCode, new RoomOptions { MaxPlayers = m_maxPlayersPerRoom });
        }
    }
    #endregion


    #region Public Methods
    public void CreateNewLobby(string lobbyCode)
    {
        Debug.Log("In lobby: " + PhotonNetwork.InRoom);
        if (!PhotonNetwork.InRoom)
        {
            m_roomCode = GenerateCode();
            PhotonNetwork.CreateRoom(m_roomCode, new RoomOptions { MaxPlayers = m_maxPlayersPerRoom });
        }
        else
        {
            Debug.Log("Already in a room");
        }
    }
    #endregion


    #region Private Methods
    private void Connect()
    {
        //Checks if the client is connected, if connected it will join a random room else it will attempt to connect to the server
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Client already connected");
        }
        else
        {
            Debug.Log("Client connecting");
            m_isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = m_gameVersion;
        }
    }

    private string GenerateCode()
    {
        string generatedCode = "";

        //Generate a code of length equal to the value set in the MenuLobbyController
        for (int i = 0; i < this.GetComponent<MenuLobbyController>().MaxCodeLength; i++)
        {
            generatedCode += Random.Range(1, 5);        
        }

        return generatedCode;
    }
    #endregion
}
