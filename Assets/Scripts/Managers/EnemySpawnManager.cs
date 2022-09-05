using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int countEnemies = 0;    
   
    void Update()
    {
        SpawnEnemyWave();        
    }

    private void SpawnEnemyWave()
    {
        countEnemies = FindObjectsOfType<EnemyController>().Length;

        if (countEnemies == 0)
        {
            StartCoroutine(GameManagerAsteroids.IRotateSkybox(40));
            for (int i = 0; i <= GameManagerAsteroids.Instance.waveNumber; i++)
            {    
                Instantiate(enemyPrefab, GenerateEnemySpawnPosition(), enemyPrefab.transform.rotation);
            }
            GameManagerAsteroids.Instance.waveNumber++;
            GameManagerAsteroids.Instance.numberOfWeapons ++;
        }
    }

    private Vector3 GenerateEnemySpawnPosition()
    {

        float x = Random.Range(-GameManagerAsteroids.Instance.borderX, GameManagerAsteroids.Instance.borderX);
        float z = Random.Range(GameManagerAsteroids.Instance.borderZ, GameManagerAsteroids.Instance.borderZ * 2 + transform.position.z);
        return new Vector3(x*0.75f, 0, z * 3);
    }

}
