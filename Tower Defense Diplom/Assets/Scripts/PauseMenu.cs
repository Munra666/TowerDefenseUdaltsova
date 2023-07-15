using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    public FaderScene faderScene;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

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

    public void Menu()
    {
        TogglePause();
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        faderScene.FadeTo(0);
    }
}
