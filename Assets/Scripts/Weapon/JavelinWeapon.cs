using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JavelinWeapon : Weapon
{
    public float bulletSpeed = 1;
    public GameObject bulletPrefab;

    private GameObject _bulletGo;
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
                ChangeBulletEquipedState(value);
                _firstTimeFlag = false;
                _hasEquipedTemp = value;
            }
            if (_hasEquipedTemp != value)
            {
                ChangeBulletEquipedState(value);
                _hasEquipedTemp = value;
            }
        }
    }

    public bool HasEjected = false;

    private void Start()
    {
        SpawnBullet();
        _parentTransform = transform.parent;

        // 检查父物体是否存在
        if (_parentTransform != null)
        {
            if ((_parentTransform.name == "WeaponL" || _parentTransform.name == "WeaponR") && _parentTransform.parent.name == "Player")
            {
                HasEquiped = true;
            }
            else
            {
                if (!HasEjected)
                {
                    HasEquiped = false;
                }
            }
        }
        else
        {
            if (!HasEjected)
            {
                HasEquiped = false;
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
            else
            {
                if (!HasEjected)
                {
                    HasEquiped = false;
                }
            }
        }
        else
        {
            if (!HasEjected)
            {
                HasEquiped = false;
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void Attack()
    {
        if (_bulletGo == null) return;
        _bulletGo.transform.parent = null;
        _bulletGo.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        _bulletGo = null;
        HasEjected = true;
        _bulletGo.GetComponent<JavelinBullet>().HasEjected = HasEjected;
        Invoke(nameof(SpawnBullet), 0.5f);
    }

    private void SpawnBullet()
    {
        _bulletGo = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        // 为子弹添加时间戳
        long epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
        long epochSeconds = epoch / TimeSpan.TicksPerSecond;
        long epochMilliseconds = epoch / TimeSpan.TicksPerMillisecond;

        string unixTimestampMilliseconds = epochMilliseconds.ToString();
        _bulletGo.name = "JavelinBullet" + unixTimestampMilliseconds;
        _bulletGo.transform.parent = transform;
        if (this.tag == Tag.INTERACTABLE)
        {
            _bulletGo.tag = Tag.INTERACTABLE;
            PickableObject po = _bulletGo.AddComponent<PickableObject>();
            po.itemSO = GetComponent<PickableObject>().itemSO;
        }
    }

    private void ChangeBulletEquipedState(bool status)
    {
        _bulletGo.GetComponent<JavelinBullet>().SetEquiped(status);
        Rigidbody bulletRigid = _bulletGo.GetComponent<Rigidbody>();
        if (status)
        {
            bulletRigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX |
                                      RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            bulletRigid.constraints = RigidbodyConstraints.None;
        }
    }
}
