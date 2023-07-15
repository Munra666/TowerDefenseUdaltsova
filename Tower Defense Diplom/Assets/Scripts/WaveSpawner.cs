using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public EnemyController[] typesEnemies;
    public int newEnemy = 5;
    private bool enteringNewEnemy;
    private int difficulty = 1;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCowntdownText;

    private int waveIndex = 0;

    void Update()
    {
        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCowntdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        Wave wave = new Wave();
        wave.countEnemies = waveIndex + 1;
        waveIndex++;

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
            //Debug.Log(i + " " + wave.enemies.Count); //вот здесь какая-то ошибка, индекс не входит в диапазон, хотя он входит
            yield return new WaitForSeconds(1f);
        }
    }

    private void SpawnEnemy(int index, Wave wave)
    {
        Instantiate(wave.enemies[index], spawnPoint.position, spawnPoint.rotation);
    }
}
