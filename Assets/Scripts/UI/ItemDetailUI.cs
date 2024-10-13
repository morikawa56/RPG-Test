using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : MonoBehaviour
{
    public Image IconImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI TypeText;
    public TextMeshProUGUI DescriptionText;
    public GameObject PropertyGrid;
    public GameObject PropertyTemplate;

    private ItemScriptObject _itemSO;
    private ItemUI _itemUI;

    private void Start()
    {
        PropertyTemplate.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void UpdateItemDetailUI(ItemScriptObject itemSO, ItemUI itemUI)
    {
        this._itemSO = itemSO;
        this._itemUI = itemUI;

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
        DescriptionText.text = itemSO.description;
        foreach (Transform child in PropertyGrid.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (var property in itemSO.propertyList)
        {
            string propertyStr = "";
            string propertyType = "";
            switch (property.propertyType)
            {
                case PropertyType.HPValue:
                    propertyType = "HP";
                    break;
                case PropertyType.EnergyValue:
                    propertyType = "EP";
                    break;
                case PropertyType.MentalValue:
                    propertyType = "MP";
                    break;
                case PropertyType.AttackValue:
                    propertyType = "AP";
                    break;
                case PropertyType.SpeedValue:
                    propertyType = "SP";
                    break;
                default:
                    break;
            }

            string propertyValue = "";
            if (property.value >= 0)
            {
                propertyValue = "+" + property.value.ToString();
            }
            else
            {
                propertyValue = property.value.ToString();
            }

            propertyStr = propertyType + propertyValue;
            GameObject go = GameObject.Instantiate(PropertyTemplate);
            go.SetActive(true);
            go.transform.SetParent(PropertyGrid.transform);
            go.transform.localScale = Vector3.one;
            go.transform.Find("Property").GetComponent<TextMeshProUGUI>().text = propertyStr;
            this.gameObject.SetActive(true);
        }
    }
    public void OnUseButtonClick()
    {
        InventoryUI.Instance.OnItemUse(_itemSO, _itemUI);
        this.gameObject.SetActive(false);
    }
}
