using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Tooltip("Offset value for hud object")]
    public Vector3 m_Offset = Vector3.up * 2;

    [Tooltip("Slider for health value")]
    public Slider slider;

    private float m_value = 0;

    public float Value
    {
        get
        {
            return m_value;
        }
        set
        {
            if (m_value != value)
            {
                slider.value = value;
            }
            m_value = value;

        }
    }


    private Character character;
    private bool m_IsSetup = false;

    #region MonoBehaviour Call Backs
    void LateUpdate()
    {
        refreshPosition();
    }

    #endregion

    #region Setups
    public void Set(Character character)
    {
        this.character = character;
        m_IsSetup = true;
        slider.maxValue = character.health.MaxHelath;
        slider.value = character.health.CurrentHealth;
    }
    #endregion

    #region Actions
    public void refreshPosition()
    {
        if (!m_IsSetup) return;
        if (character == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector2 pos = Camera.main.WorldToScreenPoint(character.transform.position + m_Offset);
        transform.position = pos;
        Value = character.health.CurrentHealth;
    }


    #endregion



}
