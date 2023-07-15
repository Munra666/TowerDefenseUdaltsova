using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SlowTime : MonoBehaviour
{
    public ParticleSystem slowTimeEffect;

    public int actionTime;
    public int cost;
    public Text textCost;

    public int reductionSpeed;

    private Button button;
    private bool isOn;

    private void Start()
    {
        button = GetComponent<Button>();

        var main = slowTimeEffect.main;
        main.duration = actionTime;

        textCost.text = "$" + cost.ToString();
    }

    private void Update()
    {
        if (PlayerStats.Money < cost || isOn == true)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void SlowTimeAbility()
    {
        slowTimeEffect.Play();
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.slowTime);

        PlayerStats.Money -= cost;

        StartCoroutine(SlowingEnemies());
    }

    private IEnumerator SlowingEnemies()
    {
        isOn = true;

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy e in enemies)
        {
            if (e != null)
            {
                e.GetComponent<NavMeshAgent>().speed = reductionSpeed;
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
