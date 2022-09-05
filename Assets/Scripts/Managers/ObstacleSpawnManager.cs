using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleSpawnManager : MonoBehaviour
{
    [SerializeField] float _wait;
    void Start()
    {
        StartCoroutine(CreateObstacleRoutine());
    }  

    private IEnumerator CreateObstacleRoutine()
    {
        // TODO Check if game is runing
        while (true)
        {
            _wait = Random.Range(0.005f, GameManagerAsteroids.Instance.asteroidFrequency);
            yield return new WaitForSeconds(_wait);
            ObjectPoolHelper.SpawnObjectFromPool(gameObject, GenerateObstacleSpawnPosition());
        }
    }

    private Vector3 GenerateObstacleSpawnPosition()
    {
        float x = Random.Range(-GameManagerAsteroids.Instance.borderX, GameManagerAsteroids.Instance.borderX);
        float y = Random.Range(-GameManagerAsteroids.Instance.borderY, GameManagerAsteroids.Instance.borderY);
        float z = Random.Range(GameManagerAsteroids.Instance.borderZ, GameManagerAsteroids.Instance.borderZ + transform.position.z);
        return new Vector3(x, y, z);
    }


}
