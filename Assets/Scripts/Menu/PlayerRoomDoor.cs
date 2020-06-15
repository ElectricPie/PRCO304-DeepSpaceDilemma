using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;

public class PlayerRoomDoor : MonoBehaviourPunCallbacks
{
    #region Private Serialized Variables
    [SerializeField]
    private float m_openYIncrease = 0;
    #endregion

    #region Private Variables
    private Vector3 m_originalPosition = new Vector3();
    private Vector3 m_openPosition = new Vector3();
    #endregion


    #region Monobehavior Callbacks
    // Start is called before the first frame update
    void Start()
    {
        m_originalPosition = this.transform.position;

        m_openPosition = m_originalPosition + new Vector3(0, m_openYIncrease, 0);

        Debug.Log("Current Door: " + m_originalPosition + " | Open Door: " + m_openPosition);
    }
    #endregion


    #region MonoBehaviourPunCallbacks Callbacks
    public override void OnJoinedRoom()
    {
        //Open the door when the player has joined a room
        OpenDoor();
    }

    public override void OnLeftRoom()
    {
        //Close the door when the player has left the room
        CloseDoor();
    }
    #endregion

    #region Private Methods
    private void OpenDoor()
    {
        

        this.transform.position = m_openPosition;
    }

    private void CloseDoor()
    {
        this.transform.position = m_originalPosition;
    }
    #endregion
}
