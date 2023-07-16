using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Sound Setting")]
    public AudioSource soundSource;
    public Slider soundSlider;

    [Header("Music Setting")]
    public AudioSource musicSource;
    public Slider musicSlider;

    [Header("Background")]
    public AudioClip background;

    [Header("UI")]
    public AudioClip clickButton;

    [Header("Game")]
    public AudioClip shoot;
    public AudioClip explosion;
    public AudioClip lazer;
    public AudioClip destructionTurret;
    public AudioClip apgradeTurret;
    public AudioClip selectTurret;
    public AudioClip noMoney;
    public AudioClip enemyDie;
    public AudioClip enemyExplosion;
    public AudioClip newWave;
    public AudioClip slowTime;
    public AudioClip airAttack;
    public AudioClip gameOver;

	public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
        
        PlayBackground();
    }

    private void Start()
    {
        LoadSetting();
    }

    public void OneShotPlay(AudioClip clip)
    {
        soundSource.pitch = Random.Range(0.9f, 1.1f);
        soundSource.PlayOneShot(clip);
    }

    public void PlayBackground()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void SetSoundValue(float value)
    {
        soundSource.volume = value;
        PlayerPrefs.SetFloat("soundSource.volume", soundSource.volume);
    }

    public void SetMusicValue(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("musicSource.volume", musicSource.volume);
    }

    public void LoadSetting()
    {

        if (soundSlider != null && musicSlider != null)
        {
            soundSlider.value = PlayerPrefs.GetFloat("soundSource.volume");
            musicSlider.value = PlayerPrefs.GetFloat("musicSource.volume");
        }

        soundSource.volume = PlayerPrefs.GetFloat("soundSource.volume");
        musicSource.volume = PlayerPrefs.GetFloat("musicSource.volume");
    }
}
