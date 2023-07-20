using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("Скорость движения")] public float speed = 70f;
    [Space(5)]
    [Tooltip("Наносимый урон")] public int damage = 50;
    [Space(5)]
    [Tooltip("Радиус взрыва, если это снаряд")] public float explosionRadius = 0f;
    [Tooltip("Эффект попадания")] public GameObject impactEffect;

    private Transform target;  //Цель попадания

    /// <summary>
    /// Поиск Цели
    /// </summary>
    /// <param name="_target">Цель</param>
    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        //Уничтожение пули при отсутствии цели
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        //Движение
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    /// <summary>
    /// Попадание в цель
    /// </summary>
    private void HitTarget()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        AudioManager.Instance.OneShotPlay(AudioManager.Instance.explosion);
        Destroy(gameObject);
    }

    /// <summary>
    /// Взрыв
    /// </summary>
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    /// <summary>
    /// Нанесение урона
    /// </summary>
    /// <param name="enemy">Цель нанесения урона</param>
    private void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null)
        {
            e.TakeDamage(damage);
        }
    }


    /// <summary>
    /// Визуализация радиуса взрыва на сцене
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
