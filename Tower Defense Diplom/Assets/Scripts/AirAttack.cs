using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirAttack : MonoBehaviour
{
    public ParticleSystem airAttackEffect;
    
    public int cost;
    public Text textCost;

    public int damage;

    private Button button;
    private bool isOn;

    private void Start()
    {
        button = GetComponent<Button>();

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

    public void AirAttackAbility()
    {
        airAttackEffect.Play();
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.airAttack);

        PlayerStats.Money -= cost;

        StartCoroutine(EnemiesDamage());
    }

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
