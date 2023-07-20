using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Tooltip("Экран паузы")] public GameObject ui;
    [Space(5)]
    [Tooltip("Переход сцен")] public FaderScene faderScene;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    //Включение и выключение паузы
    public void TogglePause()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        ui.SetActive(!ui.activeSelf);

        if(ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    /// <summary>
    /// Возвращение в меню
    /// </summary>
    public void Menu()
    {
        TogglePause();
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        PlayerPrefs.SetInt("SceneIndex", SceneManager.GetActiveScene().buildIndex);
        faderScene.FadeTo(0);
    }
}
