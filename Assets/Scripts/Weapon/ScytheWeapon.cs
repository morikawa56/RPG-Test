using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheWeapon : Weapon
{
    private const string AnimParmIsattack = "IsAttack";
    private Animator _animator;

    public int AtkValue = 30;

    private Transform _parentTransform;
    private bool _hasEquipedTemp = false;
    private bool _firstTimeFlag = true;

    public bool HasEquiped
    {
        get
        {
            return _hasEquipedTemp;
        }
        set
        {
            if (_firstTimeFlag)
            {
                _firstTimeFlag = false;
                _hasEquipedTemp = value;
            }
            if (_hasEquipedTemp != value)
            {
                _hasEquipedTemp = value;
            }
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _parentTransform = transform.parent;

        // 检查父物体是否存在
        if (_parentTransform != null)
        {
            // 查看是否在玩家手上
            if ((_parentTransform.name == "WeaponL" || _parentTransform.name == "WeaponR") && _parentTransform.parent.name == "Player")
            {
                HasEquiped = true;
            }
        }
    }

    private void Update()
    {
        _parentTransform = transform.parent;

        // 检查父物体是否存在
        if (_parentTransform != null)
        {
            if ((_parentTransform.name == "WeaponL" || _parentTransform.name == "WeaponR") && _parentTransform.parent.name == "Player")
            {
                HasEquiped = true;
            }
        }
    }

    public override void Attack()
    {
        _animator.SetTrigger(AnimParmIsattack);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.ENEMY)
        {
            other.GetComponent<Enemy>().TakeDamage(AtkValue);
        }
    }
}
