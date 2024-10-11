using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAttack _playerAttack;

    private void Start()
    {
        _playerAttack = GetComponent<PlayerAttack>();
    }

    public void UseItem(ItemScriptObject itemSO)
    {
        switch (itemSO.itemTypeSO)
        {
            case ItemType.Weapon:
                _playerAttack.LoadWeapon(itemSO);
                break;
            case ItemType.Consumable:
                break;
            default:
                break;
        }
    }
}
