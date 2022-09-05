using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPoolHelper 
{    
    public static void SpawnObjectFromPool(GameObject spawner, Vector3 spawnPosition)
    {
        GameObject pooledObject = ObjectPoolerSingleton.Instance.GetPooledObject();
        if (pooledObject != null)
        {
            pooledObject.SetActive(true);
            pooledObject.transform.position = spawner.transform.position + spawnPosition;
        }
    }
}
