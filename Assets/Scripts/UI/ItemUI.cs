using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image IconImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI TypeText;

    private ItemScriptObject _itemSO;

    public void InitItem(ItemScriptObject itemSO)
    {
        string type = "";
        switch (itemSO.itemTypeSO)
        {
            case ItemType.Weapon:
                type = "武器";
                break;
            case ItemType.Consumable:
                type = "可消耗品";
                break;
        }

        IconImage.sprite = itemSO.icon;
        NameText.text = itemSO.name;
        TypeText.text = type;
        this._itemSO = itemSO;
    }

    public void OnClick()
    {
        InventoryUI.Instance.OnItemClick(_itemSO, this);
    }
}
