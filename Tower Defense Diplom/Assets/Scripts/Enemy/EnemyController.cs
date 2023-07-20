using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour
{
    private GameObject endPoint;  //Конечная точка движения
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

    /// <summary>
    /// Движение врага
    /// </summary>
    private void Move()
    {
        agent.SetDestination(endPoint.transform.position);

        if(enemy.canvas.transform.rotation != Camera.main.transform.rotation)
            enemy.canvas.transform.rotation = Camera.main.transform.rotation;
    }

    /// <summary>
    /// Достижение кончной точки маршрута
    /// </summary>
    private void EndPoint()
    {
        PlayerStats.TakingLives(enemy.takingLives);
        WaveSpawner.EnemiesOnScene--;
        Destroy(gameObject);
    }

    /// <summary>
    /// Косание коллайдера конечной точки
    /// </summary>
    /// <param name="other">Коллайдер</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == endPoint)
        {
            EndPoint();
        }
    }
}
