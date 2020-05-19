using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    #region Private Seralized Variables
    [Tooltip("The positive health bar")]
    [SerializeField]
    private GameObject m_positiveHealth = null;
    #endregion


    #region Private Variables
    private Transform m_oringinalTransform = null;
    #endregion


    #region Monobehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {
        if (m_positiveHealth != null) {
            m_oringinalTransform = m_positiveHealth.transform;
        }
    }
    #endregion


    #region Public Methods
    /// <summary>
    /// Updates the health bar on the players wrist
    /// </summary>
    /// <param name="currentHealth"></param>
    /// <param name="maxHealth"></param>
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (currentHealth <= maxHealth && currentHealth >= 0)
        {
            //Set the scale of the health bar to be the percentage of the max health
            Vector3 newScale = m_positiveHealth.transform.localScale;
            float healthPercentage = currentHealth / maxHealth;
            newScale.z = healthPercentage;

            //Moves the health bar so it lines up with the edge of the other health bar
            Vector3 newPosition = m_positiveHealth.transform.localPosition;
            newPosition.z = -(1 - newScale.z) / 2 * 10;

            //Apply the new transform values
            m_positiveHealth.transform.localPosition = newPosition;
            m_positiveHealth.transform.localScale = newScale;
        }
    }
    #endregion
}
