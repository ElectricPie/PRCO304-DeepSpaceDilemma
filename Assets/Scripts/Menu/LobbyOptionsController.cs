using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;

public class LobbyOptionsController : UserInputConsole
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Public Methods
    public override void EnterCharacterCode(char codeValue)
    {
        //Removes a character from the string
        if (codeValue == '^')
        {
            
            StartGame();
        }
    }
    #endregion


    #region Private Methods
    private void StartGame()
    {
        //Makes sure the client is in a room
        if (PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Starting Game");
            }
            else
            {
                Debug.LogError("Client is not master");
            }
        }
        else
        {
            Debug.LogError("Not in game room");
        }
    }
    #endregion
}
