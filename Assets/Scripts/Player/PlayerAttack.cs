using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Weapon weapon;
    public void LoadWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public void UnloadWeapon()
    {
        weapon = null;
    }

    private void Update()
    {
        if (weapon != null && Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Attack();
        }
    }
}
