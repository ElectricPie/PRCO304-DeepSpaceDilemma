using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour
{
    #region Private Serialized Variables
    [Tooltip("The outline shader so only the right object have the shader")]
    [SerializeField]
    private Shader m_outlineShader = null;

    [Tooltip("The model that will have the outline shader applied")]
    [SerializeField]
    private Renderer m_renderer;
    #endregion


    #region Monobehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {
        //Gets the gameobjects renderer if none is provided
        if (m_renderer == null)
        {
            m_renderer = this.GetComponent<Renderer>();
        }

        //Sets the shader to the object so only that object has the shader and not the material
        if (m_outlineShader != null)
        {
            m_renderer.material.shader = m_outlineShader;
        }

        EnableOutline(1.05f, Color.yellow);
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
