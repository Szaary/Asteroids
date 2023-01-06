using System.Collections;
using UnityEngine;


public class ObstacleSpawnManager : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;
    [SerializeField] private float wait = 0.1f;

    private void Start()
    {
        StartCoroutine(CreateObstacleRoutine());
    }  

    private IEnumerator CreateObstacleRoutine()
    {
        while (true)
        {
            wait = Random.Range(0.005f, GameManager.Instance.asteroidFrequency);
            yield return new WaitForSeconds(wait);
            
            var pooledObject = pool.SpawnObjectFromPool();
            pooledObject.transform.position = GenerateObstacleSpawnPosition();
        }
    }

    private Vector3 GenerateObstacleSpawnPosition()
    {
        var x = Random.Range(-GameManager.Instance.borderX, GameManager.Instance.borderX);
        var y = Random.Range(-GameManager.Instance.borderY, GameManager.Instance.borderY);
        var z = Random.Range(GameManager.Instance.borderZ, GameManager.Instance.borderZ + transform.position.z);
        return new Vector3(x, y, z);
    }


}
