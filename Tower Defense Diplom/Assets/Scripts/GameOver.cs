using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;

    public FaderScene faderScene;

    private void OnEnable()
    {
        roundsText.text = PlayerStats.rounds.ToString();
    }

    public void Retry()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        faderScene.FadeTo(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        faderScene.FadeTo(0);
    }
}
