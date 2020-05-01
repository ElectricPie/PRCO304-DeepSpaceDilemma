using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour
{
    #region Private Serialized Variables
    [SerializeField]
    private Shader m_outlineShader;
    #endregion


    #region Private Variables
    private Renderer m_renderer = null;
    private Shader m_originalShader = null;
    #endregion


    #region Monobehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = this.GetComponent<Renderer>();
        m_originalShader = m_renderer.material.shader;

        Invoke("ChangeShader", 2.0f);
        Invoke("ChangeShaderBack", 4.0f);
    }
    #endregion


    #region Private Methods
    private void ChangeShader()
    {
        if (m_renderer != null && m_outlineShader != null)
        {
            //Changes the current shader to the outline shader
            m_renderer.material.shader = m_outlineShader;
            //Sets the colour of the outline shader
            m_renderer.material.SetColor("_OutlineColor", Color.green);
        }
    }

    private void ChangeShaderBack()
    {
        if (m_renderer != null && m_originalShader != null)
        {
            //Changes the shader back to the original shader
            m_renderer.material.shader = m_originalShader;
        }
    }
    #endregion
}
