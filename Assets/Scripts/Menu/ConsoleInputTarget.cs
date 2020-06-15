using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleInputTarget : MonoBehaviour, ITriggerTarget
{
    #region Private Vairables
    [Tooltip("The character that this input will sent to the console controller")]
    [SerializeField]
    private char m_inputCharacter;

    [Tooltip("The console controller")]
    [SerializeField]
    private UserInputConsole m_controller;
    #endregion


    #region Trigger Methods
    public void Activate()
    {
        m_controller.EnterCharacterCode(m_inputCharacter);
    }

    public void Deactivate()
    {
        //Not Needed
    }
    #endregion
}
