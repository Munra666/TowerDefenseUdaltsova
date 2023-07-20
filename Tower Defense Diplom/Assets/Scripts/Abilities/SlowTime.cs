using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SlowTime : MonoBehaviour
{
    [Tooltip("Визуализация эффекта")] public ParticleSystem slowTimeEffect;
    [Space(5)]
    [Tooltip("Время действия эффекта")] public int actionTime;
    [Tooltip("Стоимость способности")] public int cost;
    [Tooltip("Текст стоимости способности")] public Text textCost;
    [Space(5)]
    [Tooltip("Единица сокращения скорости врагов")] public int reductionSpeed;

    private Button button;  //Кнопка способности
    private bool isOn;  //Включен ли эффект

    private void Start()
    {
        button = GetComponent<Button>();

        var main = slowTimeEffect.main;
        main.duration = actionTime;

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
    /// Замедление времени
    /// </summary>
    public void SlowTimeAbility()
    {
        slowTimeEffect.Play();
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.slowTime);

        PlayerStats.Money -= cost;

        StartCoroutine(SlowingEnemies());
    }

    /// <summary>
    /// Поиск всех врагов и сокращение их скорости
    /// </summary>
    /// <returns></returns>
    private IEnumerator SlowingEnemies()
    {
        isOn = true;

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy e in enemies)
        {
            if (e != null)
            {
                float newSpeed = (e.GetComponent<NavMeshAgent>().speed / 100) * reductionSpeed;
                e.GetComponent<NavMeshAgent>().speed = newSpeed;
            }
        }

        yield return new WaitForSeconds(actionTime);

        foreach (Enemy e in enemies)
        {
            if (e != null)
            {
                e.GetComponent<NavMeshAgent>().speed = e.startSpeed;
            }
        }

        isOn = false;
    }
}
