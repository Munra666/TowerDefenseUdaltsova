using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Sound Setting")]
    public AudioSource soundSource;
    [Tooltip("Слайдер в главном меню который будет отвечать за регулирование громкости звуков")] public Slider soundSlider;

    [Header("Music Setting")]
    public AudioSource musicSource;
    [Tooltip("Слайдер в главном меню который будет отвечать за регулирование громкости музыки")] public Slider musicSlider;

    [Header("Background")]
    [Tooltip("Фоновая музыка")] public AudioClip background;

    [Header("UI")]
    [Tooltip("Звук нажатия на кнопки")] public AudioClip clickButton;

    [Header("Game")]  //Все игровые звуки
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
    public AudioClip takingLives;
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
        if(PlayerPrefs.HasKey("soundSource.volume") && PlayerPrefs.HasKey("musicSource.volume"))
            LoadSetting();
    }

    /// <summary>
    /// Одиночное проигрование звука
    /// </summary>
    /// <param name="clip">Проигроваемый клип</param>
    public void OneShotPlay(AudioClip clip)
    {
        soundSource.pitch = Random.Range(0.9f, 1.1f);
        soundSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Проигрование музыки на фоне
    /// </summary>
    public void PlayBackground()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    /// <summary>
    /// Регулирование громкости звуковых эффектов
    /// </summary>
    /// <param name="value">Значение громкости</param>
    public void SetSoundValue(float value)
    {
        soundSource.volume = value;
        PlayerPrefs.SetFloat("soundSource.volume", soundSource.volume);
    }

    /// <summary>
    /// Регулирование громкости музыки
    /// </summary>
    /// <param name="value">Значение громкости</param>
    public void SetMusicValue(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("musicSource.volume", musicSource.volume);
    }

    /// <summary>
    /// Загрузка значений громкости
    /// </summary>
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
