using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JavelinWeapon : Weapon
{
    public float bulletSpeed = 1;
    public GameObject bulletPrefab;

    private GameObject bulletGo;
    public bool hasEquiped = false;

    private void Start()
    {
        SpawnBullet();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void Attack()
    {
        if (bulletGo == null) return;
        bulletGo.transform.parent = null;
        bulletGo.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        bulletGo = null;
        Invoke(nameof(SpawnBullet), 0.5f);
    }

    private void SpawnBullet()
    {
        bulletGo = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletGo.transform.parent = transform;
    }
}
