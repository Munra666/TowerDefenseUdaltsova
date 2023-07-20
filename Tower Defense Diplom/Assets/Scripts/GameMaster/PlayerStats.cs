using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;  //Количество денег
    [Tooltip("Начальная сумма денег")] public int startMoney = 500;
    [Space(5)]
    public static int Lives;  //Количество жизней
    [Tooltip("Количество жизней в начале игры")] public int startLives = 20;
    [Space(5)]
    public static int rounds;  //Количество пройденных волн
    public static int bestResult;  //Лучший результат

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;

        rounds = 0;

        if (PlayerPrefs.GetInt("bestResult") != 0)
            bestResult = PlayerPrefs.GetInt("bestResult");

        Load();
    }

    /// <summary>
    /// Сохранение Данных
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.SetInt("Lives", Lives);
        PlayerPrefs.SetInt("rounds", rounds);
    }

    /// <summary>
    /// Загрузка данных
    /// </summary>
    private void Load()
    {
        if(PlayerPrefs.GetInt("Money") != 0 && PlayerPrefs.GetInt("Lives") != 0 && PlayerPrefs.GetInt("rounds") != 0)
        {
            Money = PlayerPrefs.GetInt("Money");
            Lives = PlayerPrefs.GetInt("Lives");
            rounds = PlayerPrefs.GetInt("rounds");
        }
    }

    /// <summary>
    /// Отнятие жизней игрока
    /// </summary>
    /// <param name="lives">Количество отнимаемых жизней</param>
    public static void TakingLives(int lives)
    {
        Lives -= lives;
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.takingLives);
        GameManager.isDamageBase = true;
    }

    private void OnDisable()
    {
        Save();
    }
}
