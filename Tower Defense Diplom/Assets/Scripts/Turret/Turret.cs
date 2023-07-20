using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    private Transform target;  //Цель слежения
    private Enemy targetEnemy;

    [Header("UI")]
    public Canvas canvas;

    [Header("General")]
    [Tooltip("Диапозон слежения и атаки")] public float range = 15f;
    [Tooltip("Стартовое количетво жизней")] public float startHealth = 100;
    [Tooltip("Количество жизней во время игры (Должно совпадать со стартовым)")] public float health;
    [Tooltip("Визуализация количества жизней")] public Image healthBar;
    [HideInInspector]
    public bool isDestruction = false;  //Разрушена ли туррель

    [Header("Use Bullets (default)")]
    [Tooltip("Ссылка на использованную пулю/снаряд")] public GameObject bulletPrefab;
    [Tooltip("Скорострельность")] public float fireRate = 1f;
    private float fireCountdown = 0f;  //Счетчик

    [Header("Use Lazer")]
    [Tooltip("Используется ли лазер")] public bool useLazer = false;
    [Tooltip("Единица урона в единицу времени")] public int damageOverTime = 20;
    [Tooltip("Лазер")] public LineRenderer lineRenderer;
    [Tooltip("Эффект попадания")] public ParticleSystem impactEffect;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";
    [Space(5)]
    [Tooltip("Вращающаяся верхняя часть туррели")] public Transform partToRotate;
    [Tooltip("Скорость вращения")] public float turnSpeed = 10f;
    [Space(5)]
    [Tooltip("Точка огня")] public Transform firePoint;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        if (canvas.transform.rotation != Camera.main.transform.rotation)
            canvas.transform.rotation = Camera.main.transform.rotation;
    }

    /// <summary>
    /// Поиск цели
    /// </summary>
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            if(useLazer)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                }  
            }
            
            return;
        }

        LockOnTarget();

        if(useLazer)
        {
            Lazer();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Поворот на цель
    /// </summary>
    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    /// <summary>
    /// Использование лазера
    /// </summary>
    private void Lazer()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            AudioManager.Instance.OneShotPlay(AudioManager.Instance.lazer);
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized * 1f;
    }

    /// <summary>
    /// Выстрел
    /// </summary>
    private void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        AudioManager.Instance.OneShotPlay(AudioManager.Instance.shoot);

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    /// <summary>
    /// Получение урона
    /// </summary>
    /// <param name="amount">Единица нанесенного урона</param>
    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        LoadSaveNodes.Instance.SaveHealthTurrets();

        if (health <= 0f)
        {
            isDestruction = true;
        }
    }

    /// <summary>
    /// Визуализация Диапозона слежения на сцене
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
