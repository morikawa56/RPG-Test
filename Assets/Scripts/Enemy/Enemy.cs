using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // 正常状态
    // 1-移动
    // 2-休息

    public enum EnemyState
    {
        NormalState,
        FightingState,
        MovingState,
        RestingState
    }

    private EnemyState state = EnemyState.NormalState;

    private EnemyState childState = EnemyState.RestingState;
    // Start is called before the first frame update
    private NavMeshAgent enemyAgent;

    public float restTime = 2;
    private float restTimer = 0;

    public int HP = 100;

    public int Exp = 10;
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyState.NormalState)
        {
            restTimer += Time.deltaTime;
            if (childState == EnemyState.RestingState)
            {
                if (restTimer > restTime)
                {
                    Vector3 randomPosition = FindRandomPosition();
                    enemyAgent.SetDestination(randomPosition);
                    childState = EnemyState.MovingState;
                }
            } else if (childState == EnemyState.MovingState)
            {
                if (enemyAgent.remainingDistance <= 0)
                {
                    restTimer = 0;
                    childState = EnemyState.RestingState;
                }
            }
        }

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     TakeDamage(30);
        // }
    }

    Vector3 FindRandomPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1, 1f),0, Random.Range(-1, 1f));
        return transform.position + randomDir.normalized * Random.Range(2, 5);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<Collider>().enabled = false;
        // int count = Random.Range(0, 4);
        int count = 4;
        for (int i = 0; i < count; i++)
        {
            SpawnPickableItem();
        }

        EventCenter.EnemyDied(this);
        Destroy(this.gameObject);
    }

    private void SpawnPickableItem()
    {
        ItemScriptObject item = ItemDBManger.Instance.GetRandomItem();
        Vector3 changedPosition = transform.position;
        changedPosition.y += 0.6f;
        GameObject go = GameObject.Instantiate(item.prefab, changedPosition, Quaternion.identity);
        go.tag = Tag.INTERACTABLE;
        Animator animator = go.GetComponent<Animator>();
        if(animator != null) animator.enabled = false;
        PickableObject po = go.AddComponent<PickableObject>();
        po.itemSO = item;

        Collider collider = go.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
            collider.isTrigger = false;
        }

        Rigidbody rigidbody = go.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = false;
        }
    }
}
