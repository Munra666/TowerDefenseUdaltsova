using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [Tooltip("Количество пройденных раундов")] public Text roundsText;
    [Tooltip("Лучший результат за все время")] public Text bestResultText;
    [Space(5)]
    [Tooltip("Переход сцен")] public FaderScene faderScene;

    private void OnEnable()
    {
        roundsText.text = PlayerStats.rounds.ToString();

        if(PlayerStats.rounds > PlayerStats.bestResult)
        {
            PlayerPrefs.SetInt("bestResult", PlayerStats.rounds);
            PlayerStats.bestResult = PlayerPrefs.GetInt("bestResult");
        }
        bestResultText.text = PlayerStats.bestResult.ToString();

        Time.timeScale = 0f;
    }

    /// <summary>
    /// Возвращение в меню
    /// </summary>
    public void Menu()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        Time.timeScale = 1f;
        PlayerPrefs.GetInt("DeleteAllSave", 1);
        faderScene.FadeTo(0);
    }
}
