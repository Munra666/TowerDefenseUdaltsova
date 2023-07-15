using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
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

    private AudioSource audioSource;

	public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one AudioManager in scene!");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBackground();
    }

    public void OneShotPlay(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayBackground()
    {
        audioSource.clip = background;
        audioSource.Play();
    }
}
