using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Weapon weapon;
    public Sprite WeaponIcon;
    public void LoadWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public void UnloadWeapon()
    {
        weapon = null;
    }

    public void LoadWeapon(ItemScriptObject itemSO)
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = null;
        }

        string prefabName = itemSO.prefab.name;
        Transform weaponParent;
        if (prefabName.Contains("Javelin"))
        {
            weaponParent = transform.Find("WeaponL");
        }
        else if (prefabName.Contains("Scythe"))
        {
            weaponParent = transform.Find("WeaponR");
        }
        else
        {
            weaponParent = transform.Find("WeaponL");
        }

        GameObject weaponGo = GameObject.Instantiate(itemSO.prefab);
        weaponGo.transform.parent = weaponParent;
        weaponGo.transform.localPosition = Vector3.zero;
        weaponGo.transform.localRotation = Quaternion.identity;

        this.weapon = weaponGo.GetComponent<Weapon>();
        this.WeaponIcon = itemSO.icon;
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }

    private void Update()
    {
        if (weapon != null && Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Attack();
        }
    }
}
