using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [Tooltip("Количество врагов на сцене в данную секунду")] public static int EnemiesOnScene = 0;
    [Space(5)]
    [Tooltip("Имеющиеся типы врагов по порядку сложности")] public EnemyController[] typesEnemies;
    [Tooltip("Через какое количество волн вводится новый враг")] public int newEnemy = 5;
    private bool enteringNewEnemy;  //Вводить ли нового врага в волну
    private int difficulty = 1;  //Сложность волны в зависимости от входящих типов врагов
    [Space(5)]
    [Tooltip("Точки появления врагов")] public Transform[] spawnPoints;
    [Space(5)]
    [Tooltip("Время через которое появляется новая волна")] public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    [Space(5)]
    [Tooltip("Отображение таймера новой волны")] public Text waveCowntdownText;

    private int waveIndex = 0;

    private void Start()
    {
        Load();
    }

    private void Update()
    {
        if(EnemiesOnScene > 0)
        {
            return;
        }
        else if(EnemiesOnScene <= 0 && countdown <= 0f)
        {
            EnemiesOnScene = 0;
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCowntdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        Wave wave = new Wave();
        waveIndex++;
        wave.countEnemies = waveIndex;

        for (int i = 0; i < waveIndex; i++)
        {
            wave.enemies.Add(typesEnemies[Random.Range(0, difficulty)]);
        }

        if (waveIndex % newEnemy == 0)
            enteringNewEnemy = true;
        else
            enteringNewEnemy = false;

        if (difficulty < typesEnemies.Length && enteringNewEnemy)
        {
            difficulty++;
        }

        PlayerStats.rounds++;
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.newWave);

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy(i, wave);
            yield return new WaitForSeconds(1f);
        }
    }

    private void SpawnEnemy(int index, Wave wave)
    {
        foreach(Transform sp in spawnPoints)
        {
            Instantiate(wave.enemies[index], sp.position, sp.rotation);
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("waveIndex", waveIndex);
        PlayerPrefs.SetInt("diggiculty", difficulty);
    }

    private void Load()
    {
        if(PlayerPrefs.GetInt("waveIndex") != 0 && PlayerPrefs.GetInt("diggiculty") != 0)
        {
            waveIndex = PlayerPrefs.GetInt("waveIndex");
            difficulty = PlayerPrefs.GetInt("diggiculty");
        }
    }

    private void OnDisable()
    {
        Save();
        
        EnemiesOnScene = 0;
    }
}
