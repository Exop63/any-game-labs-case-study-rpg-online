using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : Singleton<HUD>
{
    public Character Player => m_Character;




    [Tooltip("Prefab name for Healthbar of Player. Prefab must be in Resources/UI")]
    [SerializeField] private string m_HealthBarPrefabName = "HealthBar";

    private Dictionary<int, HealthBar> healthBars = new Dictionary<int, HealthBar>();
    private Character m_Character;


    private Inventory m_Inventory;

    public void SetHUDPlayer(Character character)
    {
        m_Character = character;
    }

    public HealthBar AddPlayerHud(Character character)
    {
        var id = character.GetInstanceID();

        if (!healthBars.ContainsKey(id))
        {
            var preafab = Resources.Load<HealthBar>($"UI/{m_HealthBarPrefabName}");
            var healthBar = Instantiate<HealthBar>(preafab, transform);
            healthBar.Set(character);
            healthBars.Add(id, healthBar);
            return healthBar;
        }
        return null;
    }
    public void RemovePlayerHud(Character character)
    {
        var id = character.GetInstanceID();
        if (healthBars.ContainsKey(id) && healthBars.TryGetValue(id, out var healthBar))
        {
            Destroy(healthBar.gameObject);
            healthBars.Remove(id);
        }
    }

    public void HidePlayerHud(Character character)
    {
        var id = character.GetInstanceID();
        if (healthBars.ContainsKey(id) && healthBars.TryGetValue(id, out var healthBar))
        {
            healthBar.gameObject.SetActive(false);
        }
    }
    public void ShowPlayerHud(Character character)
    {
        var id = character.GetInstanceID();
        if (healthBars.ContainsKey(id) && healthBars.TryGetValue(id, out var healthBar))
        {
            healthBar.gameObject.SetActive(true);
        }
    }

    #region Inventory
    internal void SetInventory(Inventory inventory)
    {
        m_Inventory = inventory;
    }

    public void ToogleInventory()
    {
        m_Inventory.Toogle();
    }
    #endregion
}
