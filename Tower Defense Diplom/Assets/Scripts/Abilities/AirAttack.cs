using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AirAttack : MonoBehaviour
{
    [Tooltip("Эффект для визуализации")] public ParticleSystem airAttackEffect;
    [Space(5)]
    [Tooltip("Стоимость использования")] public int cost;
    [Tooltip("Текст на котором отображается стоимость")] public Text textCost;
    [Space(5)]
    [Tooltip("Урон наносимый с помощью атаки")] public int damage;

    private Button button;
    private bool isOn; //Активна ли кнопка

    private void Start()
    {
        button = GetComponent<Button>();

        textCost.text = "$" + cost.ToString();
    }

    private void Update()
    {
        //Активность кнопки 
        if (PlayerStats.Money < cost || isOn == true)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    /// <summary>
    /// Воспроизведение атаки с воздуха
    /// </summary>
    public void AirAttackAbility()
    {
        airAttackEffect.Play();
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.airAttack);

        PlayerStats.Money -= cost;

        StartCoroutine(EnemiesDamage());
    }

    /// <summary>
    /// Поиск всех врагов на карте и нанесение урона
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnemiesDamage()
    {
        isOn = true;

        yield return new WaitForSeconds(1f);

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy e in enemies)
        {
            if (e != null)
            {
                e.TakeDamage(damage);
            }
        }

        yield return new WaitForSeconds(5f);

        isOn = false;
    }
}
