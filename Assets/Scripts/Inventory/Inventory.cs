using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<string> items = new List<string>();

    [SerializeField] private RectTransform content;
    private bool m_opened = false;

    private Animator m_animator;

    private void Start()
    {
        HUD.Instance.SetInventory(this);
        m_animator = GetComponent<Animator>();
        CreateInspector();
    }

    private void CreateInspector()
    {
        GameObjectOperations.Clear(content);
        var prefab = Resources.Load<Item>("Inventory/Item");
        items.ForEach(el =>
        {
            var item = Instantiate(prefab, content);
            item.Set(el, this);
        });
    }

    internal void EquiptItem(string id)
    {
        HUD.Instance.Player.EquipWeapon(id);
        Toogle();
    }

    public void Toogle()
    {
        m_opened = !m_opened;
        m_animator.SetBool("Open", m_opened);
    }

}
