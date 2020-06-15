using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;

public class LobbyOptionsController : UserInputConsole
{
    #region Public Methods
    public override void EnterCharacterCode(char codeValue)
    {
        //Starts the game
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
            //Prevents anyone but the creator of the room from starting the game
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Starting Game");
                PhotonNetwork.LoadLevel("SpaceStation");
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
