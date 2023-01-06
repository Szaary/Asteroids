using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public static event Action<int> waveSpawned;
    public static event Action<int> enemyKilled;

    [SerializeField] private float floatingTime = 2;
    [SerializeField] private ObjectPool pool;
    [SerializeField] private List<EnemyHealth> enemies;
    
    
    private int enemiesKilled;
    private int waveNumber;
    
    private void Start()
    {
        enemies = new List<EnemyHealth>();
        SpawnEnemyWave();
    }

    private void SpawnEnemyWave()
    {
        for (var i = 0; i <= waveNumber; i++)
        {    
            var go = pool.SpawnObjectFromPool();
            go.transform.position = GenerateEnemySpawnPosition();
            var enemy = go.GetComponent<EnemyHealth>();
            enemy.manager = this;
            enemies.Add(enemy);
        }
    }
    
    private Vector3 GenerateEnemySpawnPosition()
    {
        var x = Random.Range(-GameManager.Instance.borderX, GameManager.Instance.borderX);
        var y = Random.Range(-0.3f, 0.3f);
        var z = Random.Range(GameManager.Instance.borderZ, GameManager.Instance.borderZ * 2 + transform.position.z);
        
        return new Vector3(x*0.75f, y, z * 3);
    }
    
    public void RegisterKill(EnemyHealth enemyHealth)
    {
        enemies.Remove(enemyHealth);
        enemiesKilled++;
        enemyKilled?.Invoke(enemiesKilled);
        
        if (enemies.Count == 0)
        {
            SpawnEnemyWave();
            waveNumber++;
            waveSpawned?.Invoke(waveNumber);
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
            waveSpawned?.Invoke(waveNumber);
        }
        enemyHealth.gameObject.SetActive(false);
    }
}
