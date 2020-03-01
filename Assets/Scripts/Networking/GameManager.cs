using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;


namespace Net.ObjectiveComplete.DeepSpaceDilemma
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Public Variables
        [Tooltip("The prefab to use as the player")]
        public GameObject playerPrefab;
        #endregion


        #region Monobehaviour Callbacks
        // Start is called before the first frame update
        void Start()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>MISSING</a></Color> playerPrefab Reference. Set it in Game Manager", this);
            }
            else
            {
                if (NonVRCharacterController.localPlayerInstance == null)
                {
                    Debug.LogFormat("Instantiating Local Player from {0}", SceneManagerHelper.ActiveSceneName);
                    //Instantiate a character for this client
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0.0f, 5.0f, 0.0f), Quaternion.identity, 0);
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }
        }
        #endregion


        #region Photon Callbacks
        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", newPlayer.NickName);

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);

                LoadScene();
            }
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", otherPlayer.NickName);

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);

                LoadScene();
            }
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }
        #endregion


        #region Public Methods
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        #endregion


        #region Private Methods
        private void LoadScene()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork: Trying to load scene but this is not the master client");
            }
            Debug.LogFormat("PhotonNetwork: Loading level, Player Count: {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("NetworkTestPlayers" + PhotonNetwork.CurrentRoom.PlayerCount);
        }
        #endregion
    }
}