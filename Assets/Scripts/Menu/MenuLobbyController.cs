using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(LobbyNetworkManager))]
public class MenuLobbyController : MonoBehaviour
{
    #region Private Variables
    [Tooltip("The text mesh object that will display the code")]
    [SerializeField]
    private TextMesh m_codeDisplay;

    [Tooltip("The maximum lenth of the code")]
    [SerializeField]
    private int m_maxCodeLength = 4;

    private string m_enteredCode = "";

    private LobbyNetworkManager m_lobbyNetworkManager = null;
    #endregion


    #region Monobehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {
        m_enteredCode = "";
        m_lobbyNetworkManager = this.GetComponent<LobbyNetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion


    #region Private Methods
    private void UpdateText()
    {
        m_codeDisplay.text = m_enteredCode;
    }
    #endregion


    #region Public Methods
    public void EnterCodeCharacter(char codeValue)
    {
        //Removes a character from the string
        if (codeValue == '<' && m_enteredCode.Length > 0)
        {
            m_enteredCode = m_enteredCode.Remove(m_enteredCode.Length - 1);
        }
        //TODO: Attempt to join the lobby
        else if (codeValue == '>')
        {
            
        }
        else if (codeValue == '+')
        {
            m_lobbyNetworkManager.CreateNewLobby();
        }
        else
        {
            //Limits the 
            if (m_enteredCode.Length <= m_maxCodeLength - 1)
            {
                m_enteredCode += codeValue;
            }
        }

        UpdateText();
    }
    #endregion
}
