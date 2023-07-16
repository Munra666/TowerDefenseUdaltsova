using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    public float startHealth = 100;
    private float health;

    public int worth = 50;

    public GameObject dieEffect;

    [Header("EnemyExplosion")]
    public bool isExplosion = false;
    public int damage = 10;
    public float distanceExplosion = 15f;
    public GameObject explosionEffect;

    [Header("Enemy UI")]
    public Canvas canvas;
    public Image healthBar;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0f)
        {
            if (isExplosion)
                DamageTurrets();
            
            Die();
        }
    }

    public void Slow(float pct)
    {
        agent.speed = startSpeed * (1f - pct);
    }

   private void DamageTurrets()
    {
        Turret[] turrets = GameObject.FindObjectsOfType<Turret>();

        foreach (Turret turret in turrets)
        {
            float distanceToTurret = Vector3.Distance(transform.position, turret.transform.position);
            if (distanceToTurret < distanceExplosion)
            {
                turret.TakeDamage(damage);
            }
        }

        GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        AudioManager.Instance.OneShotPlay(AudioManager.Instance.enemyExplosion);
    }

    private void Die()
    {
        PlayerStats.Money += worth;

        AudioManager.Instance.OneShotPlay(AudioManager.Instance.enemyDie);

        GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesOnScene--;

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if(isExplosion)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanceExplosion);
        }
    }
}
