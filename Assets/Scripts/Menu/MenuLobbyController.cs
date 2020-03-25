using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MenuLobbyController : MonoBehaviour
{
    #region Private Variables
    [Tooltip("The text mesh object that will display the code")]
    [SerializeField]
    private TextMesh m_codeDisplay;

    private string m_enteredCode;
    #endregion


    #region Monobehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {
        m_enteredCode = "";
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
        m_enteredCode += codeValue;
        Debug.Log("Entered: " + m_enteredCode);
        UpdateText();
    }
    #endregion
}
