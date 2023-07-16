using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public int levelToLoad = 1;

    public Canvas settingCanvas;
    
    public FaderScene faderScene;
    
    public void Play()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        faderScene.FadeTo(levelToLoad);
    }

    public void Setting()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        settingCanvas.GetComponent<Animator>().SetTrigger("Toggle");
    }

    public void Quit()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        Application.Quit();
    }
}
