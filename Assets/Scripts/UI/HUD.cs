using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : Singleton<HUD>
{


    private Dictionary<int, HealthBar> healthBars = new Dictionary<int, HealthBar>();
    [Tooltip("Prefab name for Healthbar of Player. Prefab must be in Resources/UI")]
    [SerializeField] private string m_HealthBarPrefabName = "HealthBar";





    public HealthBar AddPlayerHud(Character character)
    {
        if (!healthBars.ContainsKey(character.GetInstanceID()))
        {
            var preafab = Resources.Load<HealthBar>($"UI/{m_HealthBarPrefabName}");
            var healthBar = Instantiate<HealthBar>(preafab, transform);
            healthBar.Set(character);
            return healthBar;
        }
        return null;
    }
}
