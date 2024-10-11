using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public List<ItemScriptObject> itemList;

    public void AddItem(ItemScriptObject item)
    {
        itemList.Add(item);
        InventoryUI.Instance.AddItem(item);
    }

    public void RemoveItem(ItemScriptObject itemSO)
    {
        itemList.Remove(itemSO);
    }
}
