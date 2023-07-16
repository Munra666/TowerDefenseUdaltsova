using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public Text bestResultText;

    public FaderScene faderScene;

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

    public void Retry()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        Time.timeScale = 1f;
        faderScene.FadeTo(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        Time.timeScale = 1f;
        faderScene.FadeTo(0);
    }
}
