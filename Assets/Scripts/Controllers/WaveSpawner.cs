using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public Text waveCountdownText;

    private int waveIndex = 0;

    // List of observers
    private List<IWaveObserver> observers = new List<IWaveObserver>();

    void Start()
    {
        if (spawnPoint == null || waveCountdownText == null)
        {
            // Используем GameManager для доступа к gameFacade
            GameManager.gameFacade.FindReferences();
        }
        countdown = 2f;
    }

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            NotifyObserversWaveComplete();
            this.enabled = false;
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;
        Wave wave = waves[waveIndex];
        EnemiesAlive += wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        if (spawnPoint != null)
        {
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            NotifyObserversEnemySpawned();
        }
        else
        {
            Debug.LogError("SpawnPoint is not assigned in WaveSpawner.");
        }
    }

    public void ResetWaveState()
    {
        countdown = 2f;
        waveIndex = 0;
        EnemiesAlive = 0;
    }

    public void RegisterObserver(IWaveObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IWaveObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObserversWaveComplete()
    {
        foreach (IWaveObserver observer in observers)
        {
            observer.OnWaveComplete();
        }
    }

    private void NotifyObserversEnemySpawned()
    {
        foreach (IWaveObserver observer in observers)
        {
            observer.OnEnemySpawned();
        }
    }
}
