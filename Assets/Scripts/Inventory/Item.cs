using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string m_Id;

    private Inventory m_inventory;
    public void Set(string id, Inventory inventory)
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>($"Inventory/Icons/{id}");
        m_Id = id;
        m_inventory = inventory;
    }
    public void OnClick()
    {
        m_inventory.EquiptItem(m_Id);
    }
}
