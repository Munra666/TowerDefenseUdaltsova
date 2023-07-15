﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public int levelToLoad = 1;
    
    public FaderScene faderScene;
    
    public void Play()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        faderScene.FadeTo(levelToLoad);
    }

    public void Setting()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);

    }

    public void Quit()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        Application.Quit();
    }
}