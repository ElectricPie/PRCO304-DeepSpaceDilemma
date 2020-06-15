using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinTarget : MonoBehaviour, ITriggerTarget
{
    #region Private Seralizable Variables
    [SerializeField]
    private float m_rotationSpeed = 15;
    #endregion


    #region Monobehavior Callbacks
    // Update is called once per frame
    void Update()
    {
        Vector3 currentRotation = this.transform.rotation.eulerAngles;

        currentRotation += new Vector3(0, Time.deltaTime * m_rotationSpeed, 0);

        this.transform.rotation = Quaternion.Euler(currentRotation);
    }
    #endregion


    #region Trigger Methods
    public void Activate()
    {
        Debug.Log("Game Won");
        //TODO: Change the game to the win screen
    }

    public void Deactivate()
    {
        //No Deactivation Needed
    }
    #endregion
}
