using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Character : MonoBehaviourPunCallbacks
{
    #region Private Vairables
    [Tooltip("The starting amount of health a character has")]
    [SerializeField]
    private int m_startingHealth = 30;

    [SerializeField]
    private int m_currentHealth;
    #endregion


    #region Monobehavior Callbacks
    // Start is called before the first frame update
    protected void Start()
    {
        //Sets the characters health when starting
        m_currentHealth = m_startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion


    #region Public Methods
    /// <summary>
    /// Deals damage to the character
    /// </summary>
    /// <param name="damageValue">The amount of damage that will be dealt to the character</param>
    public void TakeDamage(int damageValue) 
    {
        m_currentHealth -= damageValue;

        //Kills the character if the health is less that or equal to 0
        if (m_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("<a>character</a> has '" + m_currentHealth + "' health left", this.gameObject);
        }
    }
    #endregion

    #region Private Methods
    private void Die()
    {
        Debug.Log("<a>character</a> has died", this.gameObject);

        //TODO: Implement death
    }
    #endregion
}
