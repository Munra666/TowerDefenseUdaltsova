using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Tooltip("Стартовая скорость движения")] public float startSpeed = 10f;
    [Space(5)]
    [Tooltip("Стартовое количество жизней")] public float startHealth = 100;
    private float health;
    [Space(5)]
    [Tooltip("Вознаграждение за убийство")] public int worth = 50;
    [Tooltip("Количество отнимаемых жизней при достижении базы")] public int takingLives = 1;
    [Space(5)]
    [Tooltip("Визуальный эффект смерти")] public GameObject dieEffect;

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

    /// <summary>
    /// Получение урона
    /// </summary>
    /// <param name="amount">Единица полученного урона</param>
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

    /// <summary>
    /// Замедление при использование способности замедления
    /// </summary>
    /// <param name="pct">процент</param>
    public void Slow(float pct)
    {
        agent.speed = startSpeed * (1f - pct);
    }

    /// <summary>
    /// Нанесение урона туррели при взрыве
    /// </summary>
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

        LoadSaveNodes.Instance.SaveHealthTurrets();
    }

    /// <summary>
    /// Смерть врага
    /// </summary>
    private void Die()
    {
        PlayerStats.Money += worth;

        AudioManager.Instance.OneShotPlay(AudioManager.Instance.enemyDie);

        GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesOnScene--;

        Destroy(gameObject);
    }

    /// <summary>
    /// Визуализация радиуса взрыва на сцене
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if(isExplosion)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanceExplosion);
        }
    }
}
