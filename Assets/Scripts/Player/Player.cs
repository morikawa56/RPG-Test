using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAttack _playerAttack;
    private PlayerProperty _playerProperty;

    private void Start()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        _playerProperty = GetComponent<PlayerProperty>();
    }

    public void UseItem(ItemScriptObject itemSO)
    {
        switch (itemSO.itemTypeSO)
        {
            case ItemType.Weapon:
                _playerAttack.LoadWeapon(itemSO);
                break;
            case ItemType.Consumable:
                _playerProperty.UseDrug(itemSO);
                break;
            default:
                break;
        }
    }
}
