using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    public static event Action AllEnemiesDestroyed;

    [SerializeField] private ObjectPool pool;
    [SerializeField] private List<EnemyController> enemies;

    private void Start()
    {
        enemies = new List<EnemyController>();
        SpawnEnemyWave();
    }

    private void SpawnEnemyWave()
    {
        for (var i = 0; i <= GameManager.Instance.waveNumber; i++)
        {    
            var go = pool.SpawnObjectFromPool();
            go.transform.position = GenerateEnemySpawnPosition();
            var enemy = go.GetComponent<EnemyController>();
            enemy.manager = this;
            enemies.Add(enemy);
        }
    }
    
    public void Remove(EnemyController enemyController)
    {
        enemies.Remove(enemyController);
        enemyController.gameObject.SetActive(false);
        
        if (enemies.Count == 0)
        {
            SpawnEnemyWave(); 
            AllEnemiesDestroyed?.Invoke();
        }
    }
    
    private Vector3 GenerateEnemySpawnPosition()
    {
        var x = Random.Range(-GameManager.Instance.borderX, GameManager.Instance.borderX);
        var z = Random.Range(GameManager.Instance.borderZ, GameManager.Instance.borderZ * 2 + transform.position.z);
        return new Vector3(x*0.75f, 0, z * 3);
    }
}
