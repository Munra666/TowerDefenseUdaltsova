using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("Окно конца игры")] public GameObject gameOverUI;
    [Space(5)]
    [Tooltip("База Игрока")] public GameObject _base;
    [HideInInspector]
    public static bool isDamageBase = false;  //Был ли нанесен урон базе
    private bool gameIsOver = false;  //Закончилась ли игра
    private Animator anim;
    

    private void Start()
    {
        gameIsOver = false;
        anim = _base.GetComponent<Animator>();
    }

    private void Update()
    {
        if (PlayerStats.Lives <= 0 && !gameIsOver)
        {
            gameIsOver = true;
            StartCoroutine(EndGame());
        }

        if (isDamageBase)
        {
            DamageBase();
        }
    }

    /// <summary>
    /// Визуализация атаки на базу
    /// </summary>
    /// <returns></returns>
    public void DamageBase()
    {
        isDamageBase = false;
        anim.SetTrigger("Damage");
    }

    /// <summary>
    /// Конец игры
    /// </summary>
    private IEnumerator EndGame()
    {
        anim.SetTrigger("Destruction");
        yield return new WaitForSecondsRealtime(1f);
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.gameOver);

        gameOverUI.SetActive(true);
    }
}
