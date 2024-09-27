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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(30);
        }
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
            GetComponent<Collider>().enabled = false;
<<<<<<< HEAD
=======
            // int count = Random.Range(0, 4);
>>>>>>> 8e00ff2e1d71052e241703f330987272c84a5322
            int count = 4;
            for (int i = 0; i < count; i++)
            {
                ItemScriptObject item = ItemDBManger.Instance.GetRandomItem();
<<<<<<< HEAD
                Vector3 currentPosition = transform.position;
                Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y + 1f, currentPosition.z);
                GameObject go = GameObject.Instantiate(item.prefab, newPosition, Quaternion.identity);
                Animator animator = go.GetComponent<Animator>();
                if(animator!=null) animator.enabled = false;
=======
                Vector3 changedPosition = transform.position;
                changedPosition.y += 0.6f;
                GameObject go = GameObject.Instantiate(item.prefab, changedPosition, Quaternion.identity);
                Animator animator = go.GetComponent<Animator>();
                if(animator != null) animator.enabled = false;
>>>>>>> 8e00ff2e1d71052e241703f330987272c84a5322
            }

            Destroy(this.gameObject);
        }
    }
}
