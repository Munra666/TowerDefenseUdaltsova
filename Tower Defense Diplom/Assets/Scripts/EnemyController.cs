using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour
{
    private GameObject endPoint;
    private NavMeshAgent agent;
    private Enemy enemy;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        endPoint = GameObject.FindGameObjectWithTag("Finish");
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        agent.SetDestination(endPoint.transform.position);

        if(enemy.canvas.transform.rotation != Camera.main.transform.rotation)
            enemy.canvas.transform.rotation = Camera.main.transform.rotation;
    }

    private void EndPoint()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesOnScene--;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == endPoint)
        {
            EndPoint();
        }
    }
}
