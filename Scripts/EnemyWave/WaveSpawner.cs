using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public float timeBetweenWaves = 10f;
    public Text waveCountDownText;
    public GameManager gameManager;
    

    private float countdown = 15f;
    private int waveIndex = 0;

    void Update()
    {
        Debug.Log(EnemiesAlive);
        if (EnemiesAlive > 0)
        {
            return;
        }
        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        waveCountDownText.text = Mathf.Round(countdown).ToString();
    }
    IEnumerator SpawnWave()
    {

        PlayerStats.Rounds ++;

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count1; i++)
        {
            SpawnEnemy(wave.enemy1);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        if (wave.enemy2)
        {
            for (int i = 0; i < wave.count2; i++)
            {
                SpawnEnemy(wave.enemy2);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        if (wave.enemy3)
        {
            for (int i = 0; i < wave.count3; i++)
            {
                SpawnEnemy(wave.enemy3);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }
        waveIndex ++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint1.position, spawnPoint1.rotation);
        EnemiesAlive++;
        if (spawnPoint2)
        {
            Instantiate(enemy, spawnPoint2.position, spawnPoint2.rotation);
            EnemiesAlive++;
        }
    }
}
