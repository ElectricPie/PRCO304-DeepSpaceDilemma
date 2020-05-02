using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour
{
    #region Private Variables
    private Renderer m_renderer = null;
    private Shader m_originalShader = null;
    #endregion


    #region Monobehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = this.GetComponent<Renderer>();
    }
    #endregion


    #region Public Methods
    public void EnableOutline(float outlineWidth, Color outlineColor)
    {
        if (m_renderer != null)
        {
            m_renderer.material.SetFloat("_OutlineWidth", outlineWidth);
            m_renderer.material.SetColor("_OutlineColor", outlineColor);
        }
    }

    public void DisableOutline()
    {
        if (m_renderer != null)
        {
            m_renderer.material.SetFloat("_OutlineWidth", 1.0f);
        }
    }
    #endregion
}
