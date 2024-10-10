using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public float SpawnTime;
    private float _spawnTimer;

    private void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer > SpawnTime)
        {
            _spawnTimer = 0;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject.Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
    }
}
