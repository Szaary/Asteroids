using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public static event Action<int, int> WaveSpawned;
    public static Action<int, int> EnemyKilled;

    public (int, int) GetEnemies => (enemiesKilled, enemiesToKill);

    [SerializeField] private float floatingTime = 2;
    [SerializeField] private ObjectPool pool;
    [SerializeField] private List<EnemyHealth> enemies;
    [SerializeField] private LevelSystem playerLevelSystem;
    public GameObject Player => playerLevelSystem.gameObject;
    
    public int finalWave;
    public int enemiesToKill;
    
    private int enemiesKilled;
    private int waveNumber;

    private void Awake()
    {
        enemies = new List<EnemyHealth>();
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState is GameState.Mission)
        {
            enemiesKilled = 0;
            waveNumber = 0;
            finalWave = 0;
            enemiesToKill = 0;
            SpawnEnemyWave();
        }
        
        else if (gameState is GameState.Victory or GameState.Defeat)
        {
            foreach (var enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
            enemies.Clear();
        }
    }

    private void SpawnEnemyWave()
    {
        if (GameManager.State is GameState.Mission)
        {
            for (var i = 0; i <= waveNumber; i++)
            {
                var go = pool.SpawnObjectFromPool();
                go.transform.position = GenerateEnemySpawnPosition();
                var health = go.GetComponent<EnemyHealth>();
                health.waveManager = this;
                enemies.Add(health);
                var controller = go.GetComponent<EnemyController>();
                controller.waveManager = this;
            }
        }
    }

    private Vector3 GenerateEnemySpawnPosition()
    {
        var x = Random.Range(-GameManager.Instance.borderX, GameManager.Instance.borderX);
        var y = Random.Range(-0.3f, 0.3f);
        var z = Random.Range(GameManager.Instance.borderZ, GameManager.Instance.borderZ * 2 + transform.position.z);

        return new Vector3(x * 0.75f, y, z * 3);
    }

    public void RegisterKill(EnemyHealth enemyHealth)
    {
        enemies.Remove(enemyHealth);
        enemiesKilled++;
        playerLevelSystem.AddExperience(enemyHealth.experienceGain);
        EnemyKilled?.Invoke(enemiesKilled, enemiesToKill);

        if (enemies.Count == 0)
        {
            SpawnEnemyWave();
            waveNumber++;
            WaveSpawned?.Invoke(waveNumber, enemiesToKill);
        }

        StartCoroutine(DestroyAfterTime(enemyHealth.gameObject, floatingTime));
    }

    private IEnumerator DestroyAfterTime(GameObject enemyHealthGameObject, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        enemyHealthGameObject.SetActive(false);
    }

    public void RegisterDestroy(EnemyHealth enemyHealth)
    {
        enemies.Remove(enemyHealth);
        if (enemies.Count == 0)
        {
            SpawnEnemyWave();
            waveNumber++;
            WaveSpawned?.Invoke(waveNumber, finalWave);
        }

        enemyHealth.gameObject.SetActive(false);
    }
}