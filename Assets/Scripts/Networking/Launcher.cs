using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [Tooltip("The maximum number of players a room can hold")]
    [SerializeField]
    private byte m_maxPlayersPerRoom = 5;
    #endregion


    #region Public Fields
    [Tooltip("The UI panel which includes the button")]
    [SerializeField]
    private GameObject m_controlPanel;
    [Tooltip("The UI lable to show the connection is progressing")]
    [SerializeField]
    private GameObject m_progressLabel;
    #endregion


    #region Private Fields
    private string gameVersion = "1";
    #endregion


    #region MonoBehaviour CallBacks
    void Awake()
    {
        //Makes sure all clients in the room use the same scene
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Connect();
        m_progressLabel.SetActive(false);
        m_controlPanel.SetActive(true);
    }
    #endregion


    #region MonoBehaviourPunCallbacks Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        m_progressLabel.SetActive(false);
        m_controlPanel.SetActive(true);

        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconect() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN PUN Basics Tutorial/Launcher: OnJoinRandomFailed() was called by PUN. No random romms available. Creating room");

        //Creates a new room if no avaible rooms are found
        PhotonNetwork.CreateRoom(null, new RoomOptions{ MaxPlayers = m_maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN PUN Basics Tutorial/Launcher: OnJoinedRoom() was called by PUN. Now in a room");
    }
    #endregion


    #region Public Methods
    public void Connect()
    {
        m_progressLabel.SetActive(true);
        m_controlPanel.SetActive(false);

        //Checks if the client is connected, if connected it will join a random room else it will attempt to connect to the server
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    #endregion
}
